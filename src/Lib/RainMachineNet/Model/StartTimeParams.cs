using System;
using System.Collections.Generic;
using System.Text;
using static RainMachineNet.Model.Shared;

namespace RainMachineNet.Model
{
    public class StartTimeParams
    {
        public StartTimeOffset offsetSign { get; set; }
        public StartTimeType type { get; set; }
        public int offsetMinutes { get; set; }
    }
}
