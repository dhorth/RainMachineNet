using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNetTest
{
    public static class Constants
    {
#if DEBUG
        public static string SSID => "<--SSID-->";
        public static string DemoNetName => "demo.labs.rainmachine.com";
        public static string NetName => "MOCK";
        public static string DeviceName => "<-- device name -->";
        public static string User => "<-- user id -->";
        public static string Password => "<-- password -->";
        public static int ProgramCount => 3;
        public static int ZoneCount => 16;
        public static int TestProgram => 3;
        public static int DailyStats => 6;
        public static string TestProgramName => "Test";
        public static List<int> TestZones => new List<int> { 15, 16 };
        public static string DeviceCertId = "<-- Device cert id -->";
#else
        public static string SSID => "<--SSID-->";
        public static string DemoNetName => "demo.labs.rainmachine.com";
        public static string NetName => "<-- ipaddress -->";
        public static string DeviceName => "<-- device name -->";
        public static string User => "<-- user id -->";
        public static string Password => "<-- password -->";
        public static int ProgramCount => 3;
        public static int ZoneCount => 16;
        public static int TestProgram => 3;
        public static string TestProgramName => "Test";
        public static List<int> TestZones => new List<int>{15,16 };
        public static string DeviceCertId = "<-- Device cert id -->";
#endif
    }
}
