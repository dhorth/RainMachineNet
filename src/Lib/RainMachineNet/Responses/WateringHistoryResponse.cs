using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class WateringHistoryResponse: ResponseBase
    {
        public List<PastValue> pastValues { get; set; }
    }
}
