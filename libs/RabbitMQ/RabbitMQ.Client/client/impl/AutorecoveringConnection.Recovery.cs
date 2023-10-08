// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client.Impl;
using RabbitMQ.Client.Logging;

namespace RabbitMQ.Client.Framing.Impl
{
#nullable enable
    internal sealed partial class AutorecoveringConnection
    {
        private Task? _recoveryTask;
        private CancellationTokenSource? _recoveryCancellationTokenSource;

        private CancellationTokenSource RecoveryCancellationTokenSource => _recoveryCancellationTokenSource ??= new CancellationTokenSource();

        private void HandleConnectionShutdown(object _, ShutdownEventArgs args)
        {
            if (ShouldTriggerConnectionRecovery(args))
            {
                var recoverTask = new Task<Task>(RecoverConnectionAsync);
                if (Interlocked.CompareExchange(ref _recoveryTask, recoverTask.Unwrap(), null) is null)
                {
                    recoverTask.Start();
                }
            }

            static bool ShouldTriggerConnectionRecovery(ShutdownEventArgs args) =>
                args.Initiator == ShutdownInitiator.Peer ||
                // happens when EOF is reached, e.g. due to RabbitMQ node
                // connectivity loss or abrupt shutdown
                args.Initiator == ShutdownInitiator.Library;
        }

        private async Task RecoverConnectionAsync()
        {
            try
            {
                var token = RecoveryCancellationTokenSource.Token;
                bool success;
                do
                {
                    await Task.Delay(_config.NetworkRecoveryInterval, token).ConfigureAwait(false);
                    success = TryPerformAutomaticRecovery();
                } while (!success && !token.IsCancellationRequested);
            }
            catch (OperationCanceledException)
            {
                // expected when recovery cancellation token is set.
            }
            catch (Exception e)
            {
                ESLog.Error("Main recovery loop threw unexpected exception.", e);
            }

            // clear recovery task
            _recoveryTask = null;
        }

        /// <summary>
        /// Cancels the main recovery loop and will block until the loop finishes, or the timeout
        /// expires, to prevent Close operations overlapping with recovery operations.
        /// </summary>
        private void StopRecoveryLoop()
        {
            var task = _recoveryTask;
            if (task is null)
            {
                return;
            }
            RecoveryCancellationTokenSource.Cancel();

            Task timeout = Task.Delay(_config.RequestedConnectionTimeout);
            if (Task.WhenAny(task, timeout).Result == timeout)
            {
                ESLog.Warn("Timeout while trying to stop background AutorecoveringConnection recovery loop.");
            }
        }

        private static void HandleTopologyRecoveryException(TopologyRecoveryException e)
        {
            ESLog.Error("Topology recovery exception", e);
            if (e.InnerException is AlreadyClosedException ||
                    (e.InnerException is OperationInterruptedException) ||
                    (e.InnerException is TimeoutException))
            {
                throw e;
            }
            ESLog.Info($"Will not retry recovery because of {e.InnerException?.GetType().FullName}: it's not a known problem with connectivity, ignoring it", e);
        }

        private bool TryPerformAutomaticRecovery()
        {
            ESLog.Info("Performing automatic recovery");

            try
            {
                ThrowIfDisposed();
                if (TryRecoverConnectionDelegate())
                {
                    lock (_recordedEntitiesLock)
                    {
                        ThrowIfDisposed();
                        if (_config.TopologyRecoveryEnabled)
                        {
                            // The recovery sequence is the following:
                            //
                            // 1. Recover exchanges
                            // 2. Recover queues
                            // 3. Recover bindings
                            // 4. Recover consumers
                            using (var recoveryChannelFactory = new RecoveryChannelFactory(_innerConnection))
                            {
                                RecoverExchanges(recoveryChannelFactory);
                                RecoverQueues(recoveryChannelFactory);
                                RecoverBindings(recoveryChannelFactory);
                            }

                        }
                        RecoverChannelsAndItsConsumers();
                    }

                    ESLog.Info("Connection recovery completed");
                    ThrowIfDisposed();
                    _recoverySucceededWrapper.Invoke(this, EventArgs.Empty);

                    return true;
                }

                ESLog.Warn("Connection delegate was manually closed. Aborted recovery.");
            }
            catch (Exception e)
            {
                ESLog.Error("Exception when recovering connection. Will try again after retry interval.", e);
                try
                {
                    /*
                     * To prevent connection leaks on the next recovery loop,
                     * we abort the delegated connection if it is still open.
                     * We do not want to block the abort forever (potentially deadlocking recovery),
                     * so we specify the same configured timeout used for connection.
                     */
                    if (_innerConnection?.IsOpen == true)
                    {
                        _innerConnection.Abort(Constants.InternalError, "FailedAutoRecovery", _config.RequestedConnectionTimeout);
                    }
                }
                catch (Exception e2)
                {
                    ESLog.Warn("Exception when aborting previous auto recovery connection.", e2);
                }
            }

            return false;
        }

        private bool TryRecoverConnectionDelegate()
        {
            try
            {
                var defunctConnection = _innerConnection;
                IFrameHandler fh = _endpoints.SelectOne(_config.FrameHandlerFactory);
                _innerConnection = new Connection(_config, fh);
                _innerConnection.TakeOver(defunctConnection);
                return true;
            }
            catch (Exception e)
            {
                ESLog.Error("Connection recovery exception.", e);
                // Trigger recovery error events
                if (!_connectionRecoveryErrorWrapper.IsEmpty)
                {
                    _connectionRecoveryErrorWrapper.Invoke(this, new ConnectionRecoveryErrorEventArgs(e));
                }
            }

            return false;
        }

        private void RecoverExchanges(RecoveryChannelFactory recoveryChannelFactory)
        {
            foreach (var recordedExchange in _recordedExchanges.Values.Where(x => _config.TopologyRecoveryFilter?.ExchangeFilter(x) ?? true))
            {
                try
                {
                    recordedExchange.Recover(recoveryChannelFactory.RecoveryChannel);
                }
                catch (Exception ex)
                {
                    if (_config.TopologyRecoveryExceptionHandler.ExchangeRecoveryExceptionHandler != null
                        && _config.TopologyRecoveryExceptionHandler.ExchangeRecoveryExceptionCondition(recordedExchange, ex))
                    {
                        _config.TopologyRecoveryExceptionHandler.ExchangeRecoveryExceptionHandler(recordedExchange, ex, this);
                    }
                    else
                    {
                        HandleTopologyRecoveryException(new TopologyRecoveryException($"Caught an exception while recovering exchange '{recordedExchange}'", ex));
                    }
                }
            }
        }

        private void RecoverQueues(RecoveryChannelFactory recoveryChannelFactory)
        {
            foreach (var recordedQueue in _recordedQueues.Values.Where(x => _config.TopologyRecoveryFilter?.QueueFilter(x) ?? true).ToArray())
            {
                try
                {
                    var newName = recordedQueue.Recover(recoveryChannelFactory.RecoveryChannel);
                    var oldName = recordedQueue.Name;

                    if (oldName != newName)
                    {
                        // Make sure server-named queues are re-added with their new names.
                        // We only remove old name after we've updated the bindings and consumers,
                        // plus only for server-named queues, both to make sure we don't lose
                        // anything to recover. MK.
                        UpdateBindingsDestination(oldName, newName);
                        UpdateConsumerQueue(oldName, newName);

                        // see rabbitmq/rabbitmq-dotnet-client#43
                        if (recordedQueue.IsServerNamed)
                        {
                            DeleteRecordedQueue(oldName);
                        }
                        RecordQueue(new RecordedQueue(newName, recordedQueue));

                        if (!_queueNameChangeAfterRecoveryWrapper.IsEmpty)
                        {
                            _queueNameChangeAfterRecoveryWrapper.Invoke(this, new QueueNameChangedAfterRecoveryEventArgs(oldName, newName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (_config.TopologyRecoveryExceptionHandler.QueueRecoveryExceptionHandler != null
                        && _config.TopologyRecoveryExceptionHandler.QueueRecoveryExceptionCondition(recordedQueue, ex))
                    {
                        _config.TopologyRecoveryExceptionHandler.QueueRecoveryExceptionHandler(recordedQueue, ex, this);
                    }
                    else
                    {
                        HandleTopologyRecoveryException(new TopologyRecoveryException($"Caught an exception while recovering queue '{recordedQueue}'", ex));
                    }
                }
            }
        }

        private void RecoverBindings(RecoveryChannelFactory recoveryChannelFactory)
        {
            foreach (var binding in _recordedBindings.Where(x => _config.TopologyRecoveryFilter?.BindingFilter(x) ?? true))
            {
                try
                {
                    binding.Recover(recoveryChannelFactory.RecoveryChannel);
                }
                catch (Exception ex)
                {
                    if (_config.TopologyRecoveryExceptionHandler.BindingRecoveryExceptionHandler != null
                        && _config.TopologyRecoveryExceptionHandler.BindingRecoveryExceptionCondition(binding, ex))
                    {
                        _config.TopologyRecoveryExceptionHandler.BindingRecoveryExceptionHandler(binding, ex, this);
                    }
                    else
                    {
                        HandleTopologyRecoveryException(new TopologyRecoveryException($"Caught an exception while recovering binding between {binding.Source} and {binding.Destination}", ex));
                    }
                }
            }
        }

        internal void RecoverConsumers(AutorecoveringChannel channelToRecover, IChannel channelToUse)
        {
            foreach (var consumer in _recordedConsumers.Values.Where(x => _config.TopologyRecoveryFilter?.ConsumerFilter(x) ?? true).ToArray())
            {
                if (consumer.Channel != channelToRecover)
                {
                    continue;
                }

                _consumerAboutToBeRecovered.Invoke(this, new RecoveringConsumerEventArgs(consumer.ConsumerTag, consumer.Arguments));

                var oldTag = consumer.ConsumerTag;
                try
                {
                    var newTag = consumer.Recover(channelToUse);
                    UpdateConsumer(oldTag, newTag, RecordedConsumer.WithNewConsumerTag(newTag, consumer));

                    if (!_consumerTagChangeAfterRecoveryWrapper.IsEmpty)
                    {
                        _consumerTagChangeAfterRecoveryWrapper.Invoke(this, new ConsumerTagChangedAfterRecoveryEventArgs(oldTag, newTag));
                    }
                }
                catch (Exception ex)
                {
                    if (_config.TopologyRecoveryExceptionHandler.ConsumerRecoveryExceptionHandler != null
                        && _config.TopologyRecoveryExceptionHandler.ConsumerRecoveryExceptionCondition(consumer, ex))
                    {
                        _config.TopologyRecoveryExceptionHandler.ConsumerRecoveryExceptionHandler(consumer, ex, this);
                    }
                    else
                    {
                        HandleTopologyRecoveryException(new TopologyRecoveryException($"Caught an exception while recovering consumer {oldTag} on queue {consumer.Queue}", ex));
                    }
                }
            }
        }

        private void RecoverChannelsAndItsConsumers()
        {
            lock (_channels)
            {
                foreach (AutorecoveringChannel m in _channels)
                {
                    m.AutomaticallyRecover(this, _config.TopologyRecoveryEnabled);
                }
            }
        }

        private sealed class RecoveryChannelFactory : IDisposable
        {
            private readonly IConnection _connection;
            private IChannel? _recoveryChannel;

            public RecoveryChannelFactory(IConnection connection)
            {
                _connection = connection;
            }

            public IChannel RecoveryChannel
            {
                get
                {
                    if (_recoveryChannel == null)
                    {
                        _recoveryChannel = _connection.CreateChannel();
                    }

                    if (_recoveryChannel.IsClosed)
                    {
                        _recoveryChannel.Dispose();
                        _recoveryChannel = _connection.CreateChannel();
                    }

                    return _recoveryChannel;
                }
            }

            public void Dispose()
            {
                if (_recoveryChannel != null)
                {
                    _recoveryChannel.Close();
                    _recoveryChannel.Dispose();
                }
            }
        }
    }
}
