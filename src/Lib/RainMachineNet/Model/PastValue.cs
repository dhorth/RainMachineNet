using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class PastValue
    {
        public int pid { get; set; }
        public int dateTimestamp { get; set; }
        public DateTime dateTime { get; set; }
        public bool used { get; set; }
        public double et0 { get; set; }
        public double qpf { get; set; }
    }
}
