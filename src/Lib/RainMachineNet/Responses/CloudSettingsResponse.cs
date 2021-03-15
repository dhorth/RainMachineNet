using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class CloudSettingsResponse : CloudSettings, IResponseBase
    {
        public int statusCode { get; set; }
        public string message { get; set; }
    }

}
