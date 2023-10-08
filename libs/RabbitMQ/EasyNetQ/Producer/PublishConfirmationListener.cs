using System.Collections.Concurrent;
using EasyNetQ.Events;
using EasyNetQ.Internals;
using RabbitMQ.Client;

namespace EasyNetQ.Producer;

using UnconfirmedRequests = ConcurrentDictionary<ulong, TaskCompletionSource<bool>>;

/// <inheritdoc />
public class PublishConfirmationListener : IPublishConfirmationListener
{
    private readonly IDisposable[] subscriptions;

    private readonly ConcurrentDictionary<int, UnconfirmedRequests> unconfirmedChannelRequests;

    /// <summary>
    ///     Creates publish confirmations listener
    /// </summary>
    /// <param name="eventBus">The event bus</param>
    public PublishConfirmationListener(IEventBus eventBus)
    {
        unconfirmedChannelRequests = new ConcurrentDictionary<int, UnconfirmedRequests>();
        subscriptions = new[]
        {
            eventBus.Subscribe<MessageConfirmationEvent>(OnMessageConfirmation),
            eventBus.Subscribe<ChannelRecoveredEvent>(OnChannelRecovered),
            eventBus.Subscribe<ChannelShutdownEvent>(OnChannelShutdown),
            eventBus.Subscribe<ReturnedMessageEvent>(OnReturnedMessage)
        };
    }

    /// <inheritdoc />
    public IPublishPendingConfirmation CreatePendingConfirmation(IModel model)
    {
        var sequenceNumber = model.NextPublishSeqNo;

        if (sequenceNumber == 0UL)
            throw new InvalidOperationException("Confirms not selected");

        var requests = unconfirmedChannelRequests.GetOrAdd(model.ChannelNumber, _ => new UnconfirmedRequests());
        var confirmationTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        if (!requests.TryAdd(sequenceNumber, confirmationTcs))
            throw new InvalidOperationException($"Confirmation {sequenceNumber} already exists");

        return new PublishPendingConfirmation(requests, sequenceNumber, confirmationTcs);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        foreach (var subscription in subscriptions)
            subscription.Dispose();
        InterruptAllUnconfirmedRequests(true);
    }

    private void OnMessageConfirmation(in MessageConfirmationEvent @event)
    {
        if (!unconfirmedChannelRequests.TryGetValue(@event.Channel.ChannelNumber, out var requests))
            return;

        var deliveryTag = @event.DeliveryTag;
        var multiple = @event.Multiple;
        var type = @event.IsNack ? ConfirmationType.Nack : ConfirmationType.Ack;
        if (multiple)
        {
            foreach (var sequenceNumber in requests.Select(x => x.Key))
                if (sequenceNumber <= deliveryTag && requests.TryRemove(sequenceNumber, out var confirmationTcs))
                    Confirm(confirmationTcs, sequenceNumber, type);
        }
        else if (requests.TryRemove(deliveryTag, out var confirmation))
            Confirm(confirmation, deliveryTag, type);
    }

    private void OnChannelRecovered(in ChannelRecoveredEvent @event)
    {
        if (@event.Channel.NextPublishSeqNo == 0)
            return;

        InterruptUnconfirmedRequests(@event.Channel.ChannelNumber);
    }

    private void OnChannelShutdown(in ChannelShutdownEvent @event)
    {
        if (@event.Channel.NextPublishSeqNo == 0)
            return;

        InterruptUnconfirmedRequests(@event.Channel.ChannelNumber);
    }

    private void OnReturnedMessage(in ReturnedMessageEvent @event)
    {
        if (@event.Channel.NextPublishSeqNo == 0)
            return;

        if (!@event.Properties.TryGetConfirmationId(out var confirmationId))
            return;

        if (!unconfirmedChannelRequests.TryGetValue(@event.Channel.ChannelNumber, out var requests))
            return;

        if (!requests.TryRemove(confirmationId, out var confirmationTcs))
            return;

        Confirm(confirmationTcs, confirmationId, ConfirmationType.Return);
    }


    private void InterruptUnconfirmedRequests(int channelNumber, bool cancellationInsteadOfInterruption = false)
    {
        if (!unconfirmedChannelRequests.TryRemove(channelNumber, out var requests))
            return;

        do
        {
            foreach (var sequenceNumber in requests.Select(x => x.Key))
            {
                if (!requests.TryRemove(sequenceNumber, out var confirmationTcs))
                    continue;

                if (cancellationInsteadOfInterruption)
                    confirmationTcs.TrySetCanceled();
                else
                    confirmationTcs.TrySetException(new PublishInterruptedException());
            }
        } while (!requests.IsEmpty);
    }

    private void InterruptAllUnconfirmedRequests(bool cancellationInsteadOfInterruption = false)
    {
        do
        {
            foreach (var channelNumber in unconfirmedChannelRequests.Select(x => x.Key))
                InterruptUnconfirmedRequests(channelNumber, cancellationInsteadOfInterruption);
        } while (!unconfirmedChannelRequests.IsEmpty);
    }

    private static void Confirm(
        TaskCompletionSource<bool> confirmationTcs, ulong sequenceNumber, ConfirmationType type
    )
    {
        switch (type)
        {
            case ConfirmationType.Return:
                confirmationTcs.TrySetException(
                    new PublishReturnedException("Broker has signalled that message is returned")
                );
                break;
            case ConfirmationType.Nack:
                confirmationTcs.TrySetException(
                    new PublishNackedException($"Broker has signalled that publish {sequenceNumber} is nacked")
                );
                break;
            case ConfirmationType.Ack:
                confirmationTcs.TrySetResult(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private enum ConfirmationType
    {
        Ack,
        Nack,
        Return
    }

    private sealed class PublishPendingConfirmation : IPublishPendingConfirmation
    {
        private readonly UnconfirmedRequests unconfirmedRequests;
        private readonly ulong sequenceNumber;
        private readonly TaskCompletionSource<bool> confirmationTcs;

        public PublishPendingConfirmation(UnconfirmedRequests unconfirmedRequests, ulong sequenceNumber, TaskCompletionSource<bool> confirmationTcs)
        {
            this.unconfirmedRequests = unconfirmedRequests;
            this.sequenceNumber = sequenceNumber;
            this.confirmationTcs = confirmationTcs;
        }

        public ulong Id => sequenceNumber;

        public async Task WaitAsync(CancellationToken cancellationToken)
        {
            try
            {
                confirmationTcs.AttachCancellation(cancellationToken);
                await confirmationTcs.Task.ConfigureAwait(false);
            }
            finally
            {
                unconfirmedRequests.TryRemove(sequenceNumber, out _);
            }
        }

        public void Cancel()
        {
            try
            {
                confirmationTcs.TrySetCanceled();
            }
            finally
            {
                unconfirmedRequests.TryRemove(sequenceNumber, out _);
            }
        }
    }
}
