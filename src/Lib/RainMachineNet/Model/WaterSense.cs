using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Support
{
    public class WaterSense
    {
        public double fieldCapacity { get; set; }
        public int rootDepth { get; set; }
        public int minRuntime { get; set; }
        public double appEfficiency { get; set; }
        public bool isTallPlant { get; set; }
        public double permWilting { get; set; }
        public double allowedSurfaceAcc { get; set; }
        public double maxAllowedDepletion { get; set; }
        public double precipitationRate { get; set; }
        public double currentFieldCapacity { get; set; }
        public double area { get; set; }
        public int referenceTime { get; set; }
        public List<double> detailedMonthsKc { get; set; }
        public double flowrate { get; set; }
        public double soilIntakeRate { get; set; }
    }
}
