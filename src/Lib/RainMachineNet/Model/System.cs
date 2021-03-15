using System;
using System.Collections.Generic;
using System.Text;

namespace RainMachineNet.Model
{
    public class System
    {
        public bool httpEnabled { get; set; }
        public int rainSensorSnoozeDuration { get; set; }
        public bool programSingleSchedule { get; set; }
        public int mixerHistorySize { get; set; }
        public bool programZonesShowInactive { get; set; }
        public bool standaloneMode { get; set; }
        public int masterValveAfter { get; set; }
        public string apiVersion { get; set; }
        public bool selfTest { get; set; }
        public int defaultZoneWateringDuration { get; set; }
        public int maxLEDBrightness { get; set; }
        public int simulatorHistorySize { get; set; }
        public int masterValveBefore { get; set; }
        public object touchProgramToRun { get; set; }
        public bool useRainSensor { get; set; }
        public bool wizardHasRun { get; set; }
        public int waterLogHistorySize { get; set; }
        public string netName { get; set; }
        public int touchSleepTimeout { get; set; }
        public bool touchAdvanced { get; set; }
        public bool useBonjourService { get; set; }
        public int hardwareVersion { get; set; }
        public int touchLongPressTimeout { get; set; }
        public int parserDataSizeInDays { get; set; }
        public bool programListShowInactive { get; set; }
        public int parserHistorySize { get; set; }
        public int minLEDBrightness { get; set; }
        public int minWateringDurationThreshold { get; set; }
        public int localValveCount { get; set; }
        public int touchAuthAPSeconds { get; set; }
        public bool useCommandLineArguments { get; set; }
        public string databasePath { get; set; }
        public bool touchCyclePrograms { get; set; }
        public bool zoneListShowInactive { get; set; }
        public object rainSensorRainStart { get; set; }
        public List<int> zoneDuration { get; set; }
        public bool useCorrectionForPast { get; set; }
        public bool useMasterValve { get; set; }
        public int maxWateringCoef { get; set; }
    }
}
