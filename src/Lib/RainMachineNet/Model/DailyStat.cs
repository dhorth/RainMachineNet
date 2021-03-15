using System;
using System.Collections.Generic;
using System.Text;
using static RainMachineNet.Model.Shared;

namespace RainMachineNet.Model
{
    public class DailyStat
    {

        public int id { get; set; }
        public DateTime day { get; set; }
        public double mint { get; set; }
        public double maxt { get; set; }
        public int icon { get; set; }
        public double percentage { get; set; }
        public WateringFlag wateringFlag { get; set; }
        public List<int> vibration { get; set; }
        public double simulatedPercentage { get; set; }
        public List<int> simulatedVibration { get; set; }
    }
}
