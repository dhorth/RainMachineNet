using RainMachineNet.Support;
using System;
using System.Collections.Generic;
using System.Text;
using static RainMachineNet.Model.Shared;

namespace RainMachineNet.Model
{

    public class ZoneAdvanced
    {
        public int uid { get; set; }
        public string name { get; set; }
        public int valveid { get; set; }
        public double ETcoef { get; set; }
        public bool active { get; set; }
        public VegetationType type { get; set; }
        public bool internet { get; set; }
        public int savings { get; set; }
        public SlopeType slope { get; set; }
        public SunExposure sun { get; set; }
        public SoilType soil { get; set; }
        public int group_id { get; set; }
        public bool history { get; set; }
        public bool master { get; set; }
        public int before { get; set; }
        public int after { get; set; }
        public WaterSense waterSense { get; set; }
        public object customSoilPreset { get; set; }
        public object customVegetationPreset { get; set; }
        public object customSprinklerPreset { get; set; }
    }
}
