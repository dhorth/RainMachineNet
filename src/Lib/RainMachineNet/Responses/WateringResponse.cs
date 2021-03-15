using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class WateringResponse : ResponseBase
    {
        public List<Zone> zones { get; set; }
    }

}
