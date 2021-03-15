using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class Cycle
    {
        public int id { get; set; }
        public string startTime { get; set; }
        public int startTimestamp { get; set; }
        public int userDuration { get; set; }
        public int machineDuration { get; set; }
        public int realDuration { get; set; }
        public int flowclicks { get; set; }
    }
}
