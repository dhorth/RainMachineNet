<<<<<<< HEAD
﻿using Serilog;
using System;
=======
﻿using System;
>>>>>>> Add project files.
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public class WateringNotificationSubscriber<T> : RainMachineNotificationSubscriber<T> where T : WateringEvent
    {
<<<<<<< HEAD

        public WateringNotificationSubscriber():base("WateringSubscriber")
        {
=======
        public string SubscriberName { get; private set; }

        public WateringNotificationSubscriber(string subscriberName)
        {
            SubscriberName = subscriberName;
>>>>>>> Add project files.
        }

        public override void Subscribe(IObservable<T> provider)
        {
            base.Subscribe(provider);
<<<<<<< HEAD
            Log.Information($"Subscribe...");
=======
>>>>>>> Add project files.
        }

        public override void OnCompleted()
        {
<<<<<<< HEAD
            Log.Information($"OnCompleted...");
=======
            Console.WriteLine("Done");
>>>>>>> Add project files.
        }

        public override void OnError(Exception ex)
        {
<<<<<<< HEAD
            Log.Error($"OnError...", ex);
=======
            Console.WriteLine($"Error: {ex.Message}");
>>>>>>> Add project files.
        }


        public override void OnNext(T ev)
        {
<<<<<<< HEAD
            Log.Information($"OnNext...");
=======
            Console.WriteLine($"Hey {SubscriberName} -> you received {ev.EventProviderName} {ev.Description} @ {ev.Date} ");
>>>>>>> Add project files.
        }
    }
}
