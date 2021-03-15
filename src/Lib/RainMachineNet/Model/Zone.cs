using System;
using System.Collections.Generic;
using System.Text;
using static RainMachineNet.Model.Shared;

namespace RainMachineNet.Model
{
    /// <summary>
    /// Irrigation zones, each zone corresponds to a valve connector on device
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// Zone unique ID Usually from 1 to max number of valves
        /// </summary>
        public int uid { get; set; }

        /// <summary>
        /// Name of the zone
        /// </summary>
        public string name { get; set; }
        public WateringState state { get; set; }
        public bool active { get; set; }
        public int userDuration { get; set; }
        public int machineDuration { get; set; }
        public int remaining { get; set; }
        public int cycle { get; set; }
        public int noOfCycles { get; set; }
        public bool restriction { get; set; }
        public VegetationType type { get; set; }
        public bool master { get; set; }
        public bool waterSense { get; set; }
        public int flag { get; set; }
        public List<Cycle> cycles { get; set; }
    }
}
