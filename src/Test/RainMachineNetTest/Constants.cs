using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNetTest
{
    public static class Constants
    {
        public static string SSID => "MARCOHOUSE24";
        public static string DemoNetName => "demo.labs.rainmachine.com";// "192.168.1.81";
        public static string NetName => "192.168.1.81";
        public static string DeviceName => "MarcoRainMan";
        public static string User => "dhorth@horth.com";
        public static string Password => "Cassidy1";
        public static int ProgramCount => 3;
        public static int ZoneCount => 16;
        public static int TestProgram => 3;
        public static string TestProgramName => "Test";
        public static List<int> TestZones => new List<int>{15,16 };

    }
}
