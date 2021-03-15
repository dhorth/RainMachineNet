using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RainMachineNet.Responses
{
    public class ProgramsResponse : ResponseBase
    {
        public List<Program> programs { get; set; }
    }
}