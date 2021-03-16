using RainMachineNet.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RainMachineNet.Event
{
<<<<<<< HEAD
    public class WateringNotificationProvider : RainMachineNotificationProviderBase<WateringEvent>
    {
        public WateringNotificationProvider():base("Watering")
        {
        }
=======
    public class WateringNotificationProvider : IObservable<WateringEvent>
    {

        public string ProviderName { get; private set; }
        // Maintain a list of observers
        private List<IObserver<WateringEvent>> _observers;

        public WateringNotificationProvider(string _providerName)
        {
            ProviderName = _providerName;
            _observers = new List<IObserver<WateringEvent>>();
        }

        // Define Unsubscriber class
        private class Unsubscriber : IDisposable
        {

            private List<IObserver<WateringEvent>> _observers;
            private IObserver<WateringEvent> _observer;

            public Unsubscriber(List<IObserver<WateringEvent>> observers,
                                IObserver<WateringEvent> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                Dispose(true);
            }
            private bool _disposed = false;
            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }
                if (disposing)
                {
                    if (_observer != null && _observers.Contains(_observer))
                    {
                        _observers.Remove(_observer);
                    }
                }
                _disposed = true;
            }
        }

        // Define Subscribe method
        public async Task<IDisposable> SubscribeAsync(IObserver<WateringEvent> observer)
        {
            IDisposable ret=null;
            await Task.Run(() =>
            {
                ret=Subscribe(observer);
            });
            return ret;

        }


        public IDisposable Subscribe(IObserver<WateringEvent> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }

        public async Task UnSubscribeAsync(IObserver<WateringEvent> observer)
        {
            await Task.Run(() =>
            {
                UnSubscribe(observer);
            });
        }

        public void UnSubscribe(IObserver<WateringEvent> observer)
        {
            if (_observers.Contains(observer))
            {
                var idx=_observers.IndexOf(observer);
                var obj=_observers[idx];
                obj.OnCompleted();
                _observers.Remove(observer);
            }
        }



        internal bool HasSubscribers()
        {
           return _observers.Count>0;

        }

        internal void EventNotification(WateringResponse watering)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(new WateringEvent(ProviderName, watering, DateTime.Now));
            }
        }

        internal void End()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
            _observers.Clear();
        }


>>>>>>> Add project files.
    }
}
