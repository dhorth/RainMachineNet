using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public class WateringNotificationSubscriber<T> : RainMachineNotificationSubscriber<T> where T : WateringEvent
    {
        public string SubscriberName { get; private set; }

        public WateringNotificationSubscriber(string subscriberName)
        {
            SubscriberName = subscriberName;
        }

        public override void Subscribe(IObservable<T> provider)
        {
            base.Subscribe(provider);
        }

        public override void OnCompleted()
        {
            Console.WriteLine("Done");
        }

        public override void OnError(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }


        public override void OnNext(T ev)
        {
            Console.WriteLine($"Hey {SubscriberName} -> you received {ev.EventProviderName} {ev.Description} @ {ev.Date} ");
        }
    }
}
