using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class Program
    {
        public int uid { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public string startTime { get; set; }
        public int cycles { get; set; }
        public int soak { get; set; }
        public bool cs_on { get; set; }
        public int delay { get; set; }
        public bool delay_on { get; set; }
        public int status { get; set; }
        public StartTimeParams startTimeParams { get; set; }
        public Frequency frequency { get; set; }
        public int coef { get; set; }
        public bool ignoreInternetWeather { get; set; }
        public int futureField1 { get; set; }
        public int freq_modified { get; set; }
        public bool useWaterSense { get; set; }
        public string nextRun { get; set; }
        public bool simulationExpired { get; set; }
        public List<WateringTime> wateringTimes { get; set; }
        public int id { get; set; }
        public List<Zone> zones { get; set; }
    }
}
