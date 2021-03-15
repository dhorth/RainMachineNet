using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class MachineTimeResponse :  ResponseBase
    {
        public int timestamp { get; set; }
        public DateTime appDate { get; set; }
        public string timezone { get; set; }
    }
}
