using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class ScanResult
    {
        public string SSID { get; set; }
        public bool isEncrypted { get; set; }
        public string signal { get; set; }
        public bool isWEP { get; set; }
        public string BSS { get; set; }
        public bool isWPA { get; set; }
        public bool isWPA2 { get; set; }
        public string channel { get; set; }
    }
}
