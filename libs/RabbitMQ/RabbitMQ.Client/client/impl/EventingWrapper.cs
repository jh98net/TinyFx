﻿using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Client.Impl
{
#nullable enable
    internal struct EventingWrapper<T>
    {
        private event EventHandler<T>? _event;
        private Delegate[]? _handlers;
        private string? _context;
        private Action<Exception, string>? _onExceptionAction;

        public bool IsEmpty => _event is null;

        public EventingWrapper(string context, Action<Exception, string> onExceptionAction)
        {
            _event = null;
            _handlers = null;
            _context = context;
            _onExceptionAction = onExceptionAction;
        }

        public void AddHandler(EventHandler<T>? handler)
        {
            _event += handler;
            _handlers = null;
        }

        public void RemoveHandler(EventHandler<T>? handler)
        {
            _event -= handler;
            _handlers = null;
        }

        public void ClearHandlers()
        {
            _event = null;
            _handlers = null;
        }

        public void Invoke(object sender, T parameter)
        {
            var handlers = _handlers;
            if (handlers is null)
            {
                handlers = _event?.GetInvocationList();
                if (handlers is null)
                {
                    return;
                }

                _handlers = handlers;
            }
            foreach (EventHandler<T> action in handlers)
            {
                try
                {
                    action(sender, parameter);
                }
                catch (Exception exception)
                {
                    var onException = _onExceptionAction;
                    if (onException != null)
                    {
                        onException(exception, _context!);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public void Takeover(in EventingWrapper<T> other)
        {
            _event = other._event;
            _handlers = other._handlers;
            _context = other._context;
            _onExceptionAction = other._onExceptionAction;
        }
    }

    internal struct AsyncEventingWrapper<T>
    {
        private event AsyncEventHandler<T>? _event;
        private Delegate[]? _handlers;

        public bool IsEmpty => _event is null;

        public void AddHandler(AsyncEventHandler<T>? handler)
        {
            _event += handler;
            _handlers = null;
        }

        public void RemoveHandler(AsyncEventHandler<T>? handler)
        {
            _event -= handler;
            _handlers = null;
        }

        // Do not make this function async! (This type is a struct that gets copied at the start of an async method => empty _handlers is copied)
        public Task InvokeAsync(object sender, T parameter)
        {
            var handlers = _handlers;
            if (handlers is null)
            {
                handlers = _event?.GetInvocationList();
                if (handlers is null)
                {
                    return Task.CompletedTask;
                }

                _handlers = handlers;
            }

            if (handlers.Length == 1)
            {
                return ((AsyncEventHandler<T>)handlers[0])(sender, parameter);
            }
            return InternalInvoke(handlers, sender, parameter);
        }

        private static async Task InternalInvoke(Delegate[] handlers, object sender, T parameter)
        {
            foreach (AsyncEventHandler<T> action in handlers)
            {
                await action(sender, parameter).ConfigureAwait(false);
            }
        }

        public void Takeover(in AsyncEventingWrapper<T> other)
        {
            _event = other._event;
            _handlers = other._handlers;
        }
    }
}
