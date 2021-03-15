using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class WaterLogDetailResponse:ResponseBase
    {
        public WaterLog waterLog { get; set; }
    }
}
