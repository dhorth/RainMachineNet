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


        public override void OnCompleted()
        {
            Log.Information($"OnCompleted...");
        }

        public override void OnError(Exception ex)
        {
            Log.Error($"OnError...", ex);
        }


        public override void OnNext(T ev)
        {
            Log.Information($"OnNext...");
        }
    }
}
