using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public class WateringNotificationSubscriber<T> : RainMachineNotificationSubscriber<T> where T : WateringEvent
    {

        public WateringNotificationSubscriber():base("WateringSubscriber")
        {
        }

        public override void Subscribe(IObservable<T> provider)
        {
            base.Subscribe(provider);
            Log.Information($"Subscribe...");
        }

        public override void OnCompleted()
        {
            Console.WriteLine("Done");
            Log.Information($"OnCompleted...");
        }

        public override void OnError(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Log.Error($"OnError...", ex);
        }


        public override void OnNext(T ev)
        {
            Console.WriteLine($"Hey {SubscriberName} -> you received {ev.EventProviderName} {ev.Description} @ {ev.Date} ");
            Log.Information($"OnNext...");
        }
    }
}
