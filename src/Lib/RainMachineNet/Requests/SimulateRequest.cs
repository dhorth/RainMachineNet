using RainMachineNet.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Support
{
    public class SimulateRequest : RequestBase
    {
        public int ETcoef { get; set; }
        public int type { get; set; }
        public bool internet { get; set; }
        public int savings { get; set; }
        public int slope { get; set; }
        public int sun { get; set; }
        public int soil { get; set; }
        public int group_id { get; set; }
        public bool history { get; set; }
        public WaterSense waterSense { get; set; }
    }
}
