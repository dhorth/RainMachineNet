using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class Location
    {
        public int stationID { get; set; }
        public double elevation { get; set; }
        public bool doyDownloaded { get; set; }
        public string zip { get; set; }
        public double windSensitivity { get; set; }
        public double krs { get; set; }
        public string state { get; set; }
        public string stationSource { get; set; }
        public int et0Average { get; set; }
        public double latitude { get; set; }
        public int windElevation { get; set; }
        public string stationName { get; set; }
        public int wsDays { get; set; }
        public bool stationDownloaded { get; set; }
        public string address { get; set; }
        public double rainSensitivity { get; set; }
        public string timezone { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
    }
}
