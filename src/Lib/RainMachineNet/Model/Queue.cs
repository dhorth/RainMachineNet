using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class Queue
    {
        public int availableWater { get; set; }
        public int realDuration { get; set; }
        public bool running { get; set; }
        public object uid { get; set; }
        public bool restriction { get; set; }
        public bool manual { get; set; }
        public int pid { get; set; }
        public int flag { get; set; }
        public int machineDuration { get; set; }
        public int userDuration { get; set; }
        public int zid { get; set; }
        public string userStartTime { get; set; }
        public int cycles { get; set; }
        public int hwZid { get; set; }
        public int remaining { get; set; }
        public string realStartTime { get; set; }
        public int cycle { get; set; }
    }
}
