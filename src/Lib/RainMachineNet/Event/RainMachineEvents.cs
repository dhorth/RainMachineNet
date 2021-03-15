using RainMachineNet.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Event
{
    public interface IRainMachineEventBase
    {
    }

    public class WateringEvent : IRainMachineEventBase
    {
        public string EventProviderName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public WateringResponse Watering { get;set;}

        public WateringEvent(string eventProviderName, WateringResponse watering, DateTime date)
        {
            EventProviderName = eventProviderName;
            Watering = watering;
            Date = date;
        }
    }

}
