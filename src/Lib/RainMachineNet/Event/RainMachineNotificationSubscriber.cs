using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public abstract class RainMachineNotificationSubscriber<T> : IObserver<T> where T : IRainMachineEventBase
    {
        private IDisposable _unsubscriber;


        public virtual void OnCompleted()
        {
            Unsubscribe();
        }
        public abstract void OnError(Exception error);
        public abstract void OnNext(T value);

        public virtual void Subscribe(IObservable<T> provider)
        {
            // Subscribe to the Observable
            if (provider != null)
                _unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
    }
}
