using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class WateringQueueResponse : ResponseBase
    {
        public List<Model.Queue> queue { get; set; }
    }

}
