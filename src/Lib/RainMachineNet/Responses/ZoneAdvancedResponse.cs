using RainMachineNet.Model;
using RainMachineNet.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class ZoneAdvancedResponse : ZoneAdvanced, IResponseBase
    {
        public int statusCode { get; set; }
        public string message { get; set; }
    }


}
