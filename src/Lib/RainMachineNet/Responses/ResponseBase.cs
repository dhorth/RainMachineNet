using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Responses
{
    public interface IResponseBase
    {
         int statusCode { get; set; }
         string message { get; set; }
    }
    public class ResponseBase:IResponseBase
    {
        public int statusCode { get; set; }
        public string message { get; set; }
    }
}
