using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RainMachineNet.Responses
{
    public class ProgramResponse : Program, IResponseBase
    {
       public int statusCode { get; set; }
        public string message { get; set; }
    }
}