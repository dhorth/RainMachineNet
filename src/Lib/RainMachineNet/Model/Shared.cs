using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class Shared
    {
        public enum SoilType
        {
            NotSet = 0,
            ClayLoam = 1,
            SiltyClay = 2,
            Clay = 3,
            Loam = 4,
            SandyLoam = 5,
            LoamySand = 6,
            Sand = 7,
            SandyClay = 8,
            SiltLoam = 9,
            Silt = 10,
            Other = 99
        }
        public enum SprinklerType
        {
            NotSet = 0,
            PopupSpray = 1,
            Rotors = 2,
            SurfaceDrip = 3,
            Bubblers = 4,
            RotorsHigh = 5,
            Other = 99
        }
        public enum SlopeType
        {
            NotSet = 0,
            Flat = 1,
            Moderate = 2,
            High = 3,
            VeryHigh = 4,
            Other = 99
        }
        public enum SunExposure
        {
            NotSet = 0,
            FullSun = 1,
            PartialShade = 2,
            FullShade = 3
        }
        public enum VegetationType
        {
            NotSet = 0,
            NotSetOld = 1,
            Grass = 2,
            FruitTrees = 3,
            Flowers = 4,
            Vegetables = 5,
            Citrus = 6,
            Bushes = 7,
            Xeriscape = 9,
            Other = 99
        }
        public enum WateringState { NotRunning, Running, Queued };
        public enum WateringFlag
        {
            Normal_watering,
            Interrupted_by_user,
            Restriction_Threshold,
            Restriction_Freeze_Protect,
            Restriction_Day,
            Restriction_Out_Of_Day,
            Water_Surplus,
            Stopped_by_Rain_Sensor,
            Software_rain_sensor_restriction,
            Month_Restricted,
            Rain_Delay_set_by_user,
            Program_Rain_Restriction,
        }
        public enum FrequencyType
        {
            Daily = 0, Every_N_days = 1, Weekday = 2, Odd_Even = 4
        }
        public enum StartTimeType
        {
            Normal = 0, Sunrise = 1, Sunset = 2
        }
        public enum StartTimeOffset
        {
            Before = -1, Normal = 0, After = 1
        }
        public enum UpdateStatus
        {
            STATUS_IDLE = 1,STATUS_CHECKING = 2,STATUS_DOWNLOADING = 3,STATUS_UPGRADING = 4,STATUS_ERROR = 5,STATUS_REBOOT = 6
        }
    }
}
