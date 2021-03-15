using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class SimulationResponse:ResponseBase
    {
        public int referenceTime { get; set; }
        public double currentFieldCapacity { get; set; }
    }
}
