using Serilog;
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
        public RainMakerException(string msg, Exception ex) : base(msg,ex)
        {
        }
    }
    public class RainMakerLoginException : RainMakerException
    {
        public RainMakerLoginException(Exception ex) : base("Failed to Login.  Check inner details for more info.", ex)
        {
            Log.Error(Message,ex);
        }
    }

    public class RainMakerExecuteException : RainMakerException
    {
        public RainMakerExecuteException(bool hasAccessTokem, Exception ex) :
            base($"Failed to Execute. {(!hasAccessTokem?"Missing":"Valid")} access token {(!hasAccessTokem?"Make sure you login prior to calling methods":"")}  Check inner details for more info.", ex)
        {
            Log.Error(Message, ex);
        }
    }

    public class RainMakerAuthenicationException : RainMakerException
    {
        public RainMakerAuthenicationException() : base("Access Token is empty, Login before calling")
        {
            Log.Error(Message);
        }
    }
    public class RainMachineNotificationSubscriberException : RainMakerException
    {
        public RainMachineNotificationSubscriberException() : base("Provider is a required field")
        {
            Log.Error(Message);
        }
    }

}
