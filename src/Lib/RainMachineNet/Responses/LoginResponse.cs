using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public class LoginResponse : ResponseBase
    {
        public string access_token { get; set; }
        public string checksum { get; set; }
        public int expires_in { get; set; }
        public string expiration { get; set; }
    }
}
