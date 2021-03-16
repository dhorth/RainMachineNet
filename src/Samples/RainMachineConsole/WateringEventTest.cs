using RainMachineNet;
using RainMachineNet.Event;
using RainMachineNet.Responses;
using RainMachineNet.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace RainMachineConsole
{
    public class WateringEventTest : WateringNotificationSubscriber<WateringEvent>
    {
        private bool _watering;
        private int tick;
        private int zoneCount;
        public WateringEventTest()
        {
            _watering = true;
        }

        public override void OnNext(WateringEvent ev)
        {
            zoneCount=ev.Watering.zones.Max(a=>a.uid);
            foreach (var e in ev.Watering.zones)
            {
                Debugger.Log(1, "Test", $"Zone {e.uid}-{e.name} is currently {e.state}\r\n");
                Console.SetCursorPosition(5, e.uid);
                switch (e.state)
                {
                    case RainMachineNet.Model.Shared.WateringState.NotRunning:
                        Console.ForegroundColor= ConsoleColor.White;
                        break;
                    case RainMachineNet.Model.Shared.WateringState.Running:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case RainMachineNet.Model.Shared.WateringState.Queued:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                Console.WriteLine($"Zone {e.uid}-{e.name} is {e.state}");
            }
            tick++;
            if(tick>12)
                tick=1;

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, zoneCount + 1);
            var indicator= "|".PadRight(tick, '.').PadRight(12, '-') + "|";
            Console.Write(indicator);

            _watering = ev.Watering.zones.Any(a => a.state == RainMachineNet.Model.Shared.WateringState.Running);
            base.OnNext(ev);
        }

        public bool IsWatering => _watering;
    }
}
