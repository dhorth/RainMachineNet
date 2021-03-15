using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class DailyStatsResponse : ResponseBase
    {
        public List<DailyStat> DailyStats { get; set; }
    }
}
