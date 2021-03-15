using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class Day
    {
        public DateTime date { get; set; }
        public int realDuration { get; set; }
        public int dayTimestamp { get; set; }
        public int userDuration { get; set; }
        public int dateTimestamp { get; set; }
        public List<Program> programs { get; set; }
    }
}
