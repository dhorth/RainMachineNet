using RainMachineNet.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class WaterSenseResponse : WaterSense, IResponseBase
    {
        public int statusCode { get; set; }
        public string message { get; set; }

    }
}
