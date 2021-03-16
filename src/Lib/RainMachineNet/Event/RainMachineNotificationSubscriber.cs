<<<<<<< HEAD
﻿using RainMachineNet.Support;
using Serilog;
using System;
=======
﻿using System;
>>>>>>> Add project files.
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public abstract class RainMachineNotificationSubscriber<T> : IObserver<T> where T : IRainMachineEventBase
    {
        private IDisposable _unsubscriber;
<<<<<<< HEAD
        public string SubscriberName { get; private set; }

        public RainMachineNotificationSubscriber(string sub)
        {
            SubscriberName=sub;
        }

        public virtual void OnCompleted()
        {
            Log.Information($"OnCompleted...");
=======


        public virtual void OnCompleted()
        {
>>>>>>> Add project files.
            Unsubscribe();
        }
        public abstract void OnError(Exception error);
        public abstract void OnNext(T value);

        public virtual void Subscribe(IObservable<T> provider)
        {
            // Subscribe to the Observable
            if (provider != null)
<<<<<<< HEAD
            {
                _unsubscriber = provider.Subscribe(this);
                Log.Information($"Subscribe...");
            }
            else
            {
                throw new RainMachineNotificationSubscriberException();
            }
=======
                _unsubscriber = provider.Subscribe(this);
>>>>>>> Add project files.
        }

        public virtual void Unsubscribe()
        {
<<<<<<< HEAD
            Log.Information($"Unsubscribe...");
=======
>>>>>>> Add project files.
            _unsubscriber.Dispose();
        }
    }
}
