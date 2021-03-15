using Newtonsoft.Json;
using RainMachineNet.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Support
{
    public class LoginRequest:RequestBase
    {
        public LoginRequest(string userid, string pwd)
        {
            //email=userid;
            this.pwd= pwd;
            remember=true;
        }
        //public string email { get; set; }
        public string pwd { get; set; }
        public bool remember { get; set; }

    }
}
