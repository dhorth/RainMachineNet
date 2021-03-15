using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class WateringTime
    {
        public int id { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public int duration { get; set; }
        public bool active { get; set; }
        public int minRuntimeCoef { get; set; }
        public int userPercentage { get; set; }
    }
}
