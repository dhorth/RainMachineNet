using NUnit.Framework;
using RainMachineNet;
using RainMachineNet.Responses;
using RainMachineNet.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RainMachineNetTest
{
    public class RainMakerTest
    {
        private IRainMaker _rainMaker;


        [SetUp]
        public void Setup()
        {
            _rainMaker = new RainMaker();
        }
        [Test]
        public async Task Login()
        {
<<<<<<< HEAD
            var rc = await _rainMaker.LoginAsync(Constants.NetName, Constants.User, Constants.Password,Constants.DeviceCertId);
=======
            var rc = await _rainMaker.LoginAsync(Constants.NetName, Constants.User, Constants.Password);
>>>>>>> Add project files.
            Assert.IsTrue(rc);
        }
        [Test]
        public void DailyStatsException()
        {
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.DailyStats());
        }


        [Test]
        public async Task GetProvision()
        {
            await Login();
            var rc = await _rainMaker.GetProvision();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ProvisionResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.system.netName, Constants.DeviceName);
        }
        [Test]
        public async Task GetWifiSettings()
        {
            await Login();
            var rc = await _rainMaker.GetWifiSettings();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WifiSettingResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.ipAddress, Constants.NetName);
        }
        [Test]
        public async Task GetCloudSettings()
        {
            await Login();
            var rc = await _rainMaker.GetCloudSettings();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<CloudSettingsResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.email, Constants.User);
        }
        [Test]
        public async Task DailyStats()
        {
            await Login();
            var rc = await _rainMaker.DailyStats();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<DailyStatsResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.DailyStats.Count, 6);
        }
        [Test]
        public async Task GetMachineTime()
        {
            await Login();
            var rc = await _rainMaker.GetMachineTime();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<MachineTimeResponse>(rc, "Unexpected object type");
            Assert.LessOrEqual((DateTime.Now - rc.appDate).TotalSeconds, 60);
        }
        [Test]
        public async Task GetUpdateCheck()
        {
            await Login();
            var rc = await _rainMaker.GetMachineUpdateStatus();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<UpdateStatusResponse>(rc, "Unexpected object type");
            Assert.LessOrEqual((DateTime.Now - rc.lastUpdateCheck).TotalDays, 2);
        }
        [Test]
        public void SimulateException()
        {
            var request = new SimulateRequest();
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.SimulateZone(request));
        }
        [Test]
        public async Task SimulateBad()
        {
            await Login();
            var request = new SimulateRequest();
            var rc = await _rainMaker.SimulateZone(request);
            Assert.AreEqual(rc.statusCode, 1);
        }
        [Test]
        public async Task Simulate()
        {
            await Login();
            var request = new SimulateRequest();
            request.ETcoef = 1;
            request.type = 2;
            request.internet = true;
            request.savings = 50;
            request.slope = 1;
            request.sun = 1;
            request.soil = 1;
            request.group_id = 1;
            request.history = true;
            request.waterSense = new WaterSense
            {
                fieldCapacity = 0,
                rootDepth = 0,
                appEfficiency = 0,
                isTallPlant = false,
                permWilting = 0,
                maxAllowedDepletion = 0,
                precipitationRate = 0,
                allowedSurfaceAcc = 0,
                referenceTime = 661,
                detailedMonthsKc = new List<double> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
            var rc = await _rainMaker.SimulateZone(request);
            Assert.AreEqual(rc.statusCode, 0);
        }



        [Test]
        public void GetWateringStatusException()
        {
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.GetWateringStatus());
        }
        [Test]
        public async Task GetWateringStatus()
        {
            await Login();
            var rc = await _rainMaker.GetWateringStatus();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.zones.Count, Constants.ZoneCount);
        }
        [Test]
        public void GetWateringProgramException()
        {
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.GetWateringProgram());
        }
        [Test]
        public async Task GetWateringProgramInactive()
        {
            await Login();
            var rc = await _rainMaker.GetWateringProgram();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ProgramsResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.programs.Count, 0);
        }
        [Test]
        public void GetWateringQueueException()
        {
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.GetWateringQueue());
        }
        [Test]
        public async Task GetWateringQueueInactive()
        {
            await Login();
            var rc = await _rainMaker.GetWateringQueue();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.queue.Count, 0);
            Assert.AreEqual(rc.statusCode, 0);
        }
        [Test]
        public void GetWateringLogException()
        {
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.GetWateringLog());
        }



        [Test]
        public async Task GetWateringHistory()
        {
            await Login();
            var rc = await _rainMaker.GetWateringHistory();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringHistoryResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringHistoryDays()
        {
            await Login();
            var rc = await _rainMaker.GetWateringHistory(7);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringHistoryResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringHistoryDate()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringHistory(start, end);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringHistoryResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
            foreach (var day in rc.pastValues)
            {
                if (day.dateTime > DateTime.Now)
                    Assert.IsFalse(day.used);
            }
        }
        [Test]
        public async Task GetWateringHistoryDateBackwards()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringHistory(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringHistoryResponse>(rc, "Unexpected object type");

            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
            foreach (var day in rc.pastValues.Where(a => a.dateTime > DateTime.Now))
                Assert.IsFalse(day.used);
        }
        [Test]
        public async Task GetWateringHistoryDateLastMonth()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringHistory(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringHistoryResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
            foreach (var day in rc.pastValues)
            {
                if (day.dateTime > DateTime.Now)
                    Assert.IsFalse(day.used);
            }
        }



        [Test]
        public async Task GetWateringLogDays()
        {
            await Login();
            var rc = await _rainMaker.GetWateringLog(7);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDate()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLog(start, end);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDateBackwards()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLog(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDateLastMonth()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLog(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.pastValues.Count > 0, "No History Available");
        }



        [Test]
        public async Task GetWateringLogDetailsDays()
        {
            await Login();
            var rc = await _rainMaker.GetWateringLogDetails(7);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDetailsDate()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogDetails(start, end);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDetailsDateBackwards()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogDetails(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDateDetailsLastMonth()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogDetails(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }



        [Test]
        public async Task GetWateringLogSimulatedDays()
        {
            await Login();
            var rc = await _rainMaker.GetWateringLogSimulated(7);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogSimulatedDate()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogSimulated(start, end);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogSimulatedDateBackwards()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogSimulated(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDateSimulatedLastMonth()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogSimulated(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }



        [Test]
        public async Task GetWateringLogSimulatedDetailsDays()
        {
            await Login();
            var rc = await _rainMaker.GetWateringLogSimulatedDetails(7);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogSimulatedDetailsDate()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogSimulatedDetails(start, end);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogSimulatedDetailsDateBackwards()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogSimulatedDetails(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }
        [Test]
        public async Task GetWateringLogDateSimulatedDetailsLastMonth()
        {
            await Login();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var end = start.AddMonths(1).AddDays(-1);
            var rc = await _rainMaker.GetWateringLogSimulatedDetails(end, start);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WaterLogDetailResponse>(rc, "Unexpected object type");
            Assert.IsTrue(rc.waterLog.days.Count > 0, "No History Available");
        }



        [Test]
        public void GetZonesException()
        {
            Assert.ThrowsAsync<RainMakerAuthenicationException>(() => _rainMaker.GetAllZones());
        }
        [Test]
        public async Task GetAllPrograms()
        {
            await Login();
            var rc = await _rainMaker.GetAllPrograms();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ProgramsResponse>(rc, "Unexpected object type");
            Assert.AreEqual(Constants.ProgramCount, rc.programs.Count);
        }
        [Test]
        public async Task GetProgramId()
        {
            await Login();
            for (int i = 1; i <= Constants.ProgramCount; i++)
            {
                var rc = await _rainMaker.GetProgram(i);
                Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
                Assert.IsInstanceOf<ProgramResponse>(rc, "Unexpected object type");
                Assert.AreEqual(i, rc.uid);
            }
        }
        [Test]
        public async Task ProgramStart()
        {
            await Login();
            for (int i = 1; i <= Constants.ProgramCount; i++)
            {
                var rc = await _rainMaker.ProgramStart(Constants.TestProgram);
                Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
                Assert.AreEqual(rc.statusCode, 0);
            }
        }
        [Test]
        public async Task ProgramStop()
        {
            await Login();
            for (int i = 1; i <= Constants.ProgramCount; i++)
            {
                var rc = await _rainMaker.ProgramStop(Constants.TestProgram);
                Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
                Assert.AreEqual(rc.statusCode, 0);
            }
        }



        [Test]
        public async Task GetAllZones()
        {
            await Login();
            var rc = await _rainMaker.GetAllZones();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ZonesResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.Zones.Count, Constants.ZoneCount);
        }
        [Test]
        public async Task GetZoneId()
        {
            await Login();
            for (int i = 1; i <= Constants.ZoneCount; i++)
            {
                var rc = await _rainMaker.GetZoneProperties(i);
                Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
                Assert.IsInstanceOf<ZoneResponse>(rc, "Unexpected object type");
                Assert.AreEqual(i, rc.uid);
            }
        }
        [Test]
        public async Task ZoneStart()
        {
            await Login();
            var rc = await _rainMaker.ZoneStart(16, 2);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ZonesResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.statusCode, 0);
        }
        [Test]
        public async Task ZoneStop()
        {
            await Login();
            var rc = await _rainMaker.ZoneStop(16);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.AreEqual(rc.statusCode, 0);
        }
        [Test]
        public async Task ZoneStopAll()
        {
            await Login();
            var rc = await _rainMaker.ZoneStopAll();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.AreEqual(rc.statusCode, 0);
        }
        [Test]
        public async Task ZonePauseAll()
        {
            await Login();
            var rc = await _rainMaker.ZonePauseAll();
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.AreEqual(rc.statusCode, 0);
        }
        [Test]
        public async Task GetWateringProgramActive()
        {
            await Login();
            await _rainMaker.ProgramStart(Constants.TestProgram);
            var rc = await _rainMaker.GetWateringProgram();
            await _rainMaker.ProgramStop(Constants.TestProgram);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ProgramsResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.programs.Count, Constants.TestZones.Count);
        }
        [Test]
        public async Task GetWateringQueueActive()
        {
            await Login();
            await _rainMaker.ProgramStart(Constants.TestProgram);
            var rc = await _rainMaker.GetWateringQueue();
            await _rainMaker.ProgramStop(Constants.TestProgram);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<WateringQueueResponse>(rc, "Unexpected object type");
            Assert.AreEqual(rc.queue.Count, Constants.TestZones.Count);
        }
        [Test]
        public async Task GetZonePropertiesAdvanced()
        {
            await Login();
            for (int i = 1; i <= Constants.ZoneCount; i++)
            {
                var rc = await _rainMaker.GetZonePropertiesAdvanced(i);
                Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
                Assert.IsInstanceOf<ZoneAdvancedResponse>(rc, "Unexpected object type");
                Assert.AreEqual(i, rc.uid);
            }
        }


    }
}