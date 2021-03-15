using System;
using System.Collections.Generic;
using System.Text;
using static RainMachineNet.Model.Shared;

namespace RainMachineNet.Responses
{
    public class UpdateStatusResponse : ResponseBase
    {
        public int lastUpdateCheckTimestamp { get; set; }
        public List<object> packageDetails { get; set; }
        public bool update { get; set; }
        public DateTime lastUpdateCheck { get; set; }
        public UpdateStatus updateStatus { get; set; }
    }
}
