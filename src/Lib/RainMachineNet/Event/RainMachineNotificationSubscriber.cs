using RainMachineNet.Support;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public abstract class RainMachineNotificationSubscriber<T> : IObserver<T> where T : IRainMachineEventBase
    {
        private IDisposable _unsubscriber;
        public string SubscriberName { get; private set; }

        public RainMachineNotificationSubscriber(string sub)
        {
            SubscriberName=sub;
        }

        public virtual void OnCompleted()
        {
            Log.Information($"OnCompleted...");
            Unsubscribe();
        }
        public abstract void OnError(Exception error);
        public abstract void OnNext(T value);

        public virtual void Subscribe(IObservable<T> provider)
        {
            // Subscribe to the Observable
            if (provider != null)
            {
                _unsubscriber = provider.Subscribe(this);
                Log.Information($"Subscribe...");
            }
            else
            {
                throw new RainMachineNotificationSubscriberException();
            }
        }

        public virtual void Unsubscribe()
        {
            Log.Information($"Unsubscribe...");
            _unsubscriber.Dispose();
        }
    }
}
