using RainMachineNet.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RainMachineNet.Event
{
    public class WateringNotificationProvider : RainMachineNotificationProviderBase<WateringEvent>
    {

        public WateringNotificationProvider():base("Watering")
        {
        }


    }
}
