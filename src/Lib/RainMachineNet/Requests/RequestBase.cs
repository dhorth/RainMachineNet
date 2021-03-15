using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Requests
{
    public abstract class RequestBase
    {

        public virtual string ToJson()
        {
            //this sets the root object name
            return JsonConvert.SerializeObject(this);
        }
    }
}
