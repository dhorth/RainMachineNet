using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class ProvisionResponse : ResponseBase
    {
        public RainMachineNet.Model.System system { get; set; }
        public Location location { get; set; }
    }

}
