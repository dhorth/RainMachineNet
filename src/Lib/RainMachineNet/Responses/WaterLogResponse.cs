using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class WaterLogResponse:ResponseBase
    {
        public List<Day> days { get; set; }
    }
}
