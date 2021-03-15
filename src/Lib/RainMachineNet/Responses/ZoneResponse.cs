﻿using RainMachineNet.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class ZoneResponse : Zone, IResponseBase
    {
        public int statusCode { get ; set; }
        public string message { get; set; }
    }

}
