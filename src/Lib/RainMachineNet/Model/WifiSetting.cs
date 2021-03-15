using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class WifiSetting
    {
        public string @interface { get; set; }
        public string macAddress { get; set; }
        public string ipAddress { get; set; }
        public bool hasClientLink { get; set; }
        public string mode { get; set; }
    }
}
