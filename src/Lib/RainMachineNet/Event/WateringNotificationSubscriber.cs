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
<<<<<<< HEAD
=======
            Console.WriteLine("Done");
>>>>>>> fb67dfef655dd0f96eecc1212586267f4c44968f
            Log.Information($"OnCompleted...");
        }

        public override void OnError(Exception ex)
        {
<<<<<<< HEAD
=======
            Console.WriteLine($"Error: {ex.Message}");
>>>>>>> fb67dfef655dd0f96eecc1212586267f4c44968f
            Log.Error($"OnError...", ex);
        }


        public override void OnNext(T ev)
        {
<<<<<<< HEAD
=======
            Console.WriteLine($"Hey {SubscriberName} -> you received {ev.EventProviderName} {ev.Description} @ {ev.Date} ");
>>>>>>> fb67dfef655dd0f96eecc1212586267f4c44968f
            Log.Information($"OnNext...");
        }
    }
}
