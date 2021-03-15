using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Support
{
    public class RainMakerException:Exception
    {
        public RainMakerException(string msg) : base(msg)
        {

        }
    }

    public class RainMakerAuthenicationException : RainMakerException
    {
        public RainMakerAuthenicationException() : base("Access Token is empty, Login before calling")
        {

        }
    }

}
