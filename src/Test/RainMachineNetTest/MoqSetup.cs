using Moq;
using RainMachineNet.Model;
using RainMachineNet.Requests;
using RainMachineNet.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainMachineNetTest
{
    public static class MoqSetup
    {
        public static void Setup(Mock<IRestClient> client)
        {
            Setup(client, "auth/login", new LoginResponse { access_token = "test" });
            Setup(client, "dailystats", new DailyStatsResponse { DailyStats = _dailyStats });
            Setup(client, "provision", new ProvisionResponse { system = new RainMachineNet.Model.System { netName = Constants.DeviceName } });
            Setup(client, "provision/wifi", new WifiSettingResponse { ipAddress = Constants.NetName });
            Setup(client, "provision/cloud", new CloudSettingsResponse { email = Constants.User });
            Setup(client, "zone", new ZonesResponse { Zones = _zones });
            Setup(client, "program", new ProgramsResponse { programs = _programs });
            Setup(client, "machine/time", new MachineTimeResponse { appDate = DateTime.Now });
            Setup(client, "machine/update", new UpdateStatusResponse { lastUpdateCheck = DateTime.Now });

            Setup(client, $"watering/past/{DateTime.Now.ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WateringHistoryResponse { pastValues = _wateringHistory });
            Setup(client, $"watering/past/{DateTime.Now.AddDays(-Constants.DailyStats).ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WateringHistoryResponse { pastValues = _wateringHistory });
            Setup(client, $"watering/past/{DateTime.Now.ToString("yyyy-MM-dd")}/0", new WateringHistoryResponse { pastValues = new List<PastValue> { new PastValue() } });

            Setup(client, $"watering/log/{DateTime.Now.ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogResponse { days = _waterLog.days });
            Setup(client, $"watering/log/{DateTime.Now.AddDays(-Constants.DailyStats).ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogResponse { days = _waterLog.days });
            Setup(client, $"watering/log/details/{DateTime.Now.ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogDetailResponse { waterLog = _waterLog });
            Setup(client, $"watering/log/details/{DateTime.Now.AddDays(-Constants.DailyStats).ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogDetailResponse { waterLog = _waterLog });
            Setup(client, $"watering/log/simulated/{DateTime.Now.ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogDetailResponse { waterLog = _waterLog });
            Setup(client, $"watering/log/simulated/{DateTime.Now.AddDays(-Constants.DailyStats).ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogDetailResponse { waterLog = _waterLog });
            Setup(client, $"watering/log/simulated/details/{DateTime.Now.ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogDetailResponse { waterLog = _waterLog });
            Setup(client, $"watering/log/simulated/details/{DateTime.Now.AddDays(-Constants.DailyStats).ToString("yyyy-MM-dd")}/{Constants.DailyStats}", new WaterLogDetailResponse { waterLog = _waterLog });

            Setup(client, $"watering/program", new ProgramsResponse { programs = _programs });
            Setup(client, $"watering/queue", new WateringQueueResponse { queue = _queue });
            Setup(client, $"watering/zone", new WateringResponse { zones = _zones });


            Setup(client, $"zone/pauseall", new ResponseBase());
            Setup(client, $"zone/stopall", new ResponseBase());
            Setup(client, $"zone/simulate", new SimulationResponse());
            for (int i = 1; i <= Constants.ZoneCount; i++)
            {
                Setup(client, $"zone/{i}", new ZoneResponse { uid = i });
                Setup(client, $"zone/{i}/start", new ZoneResponse{uid=i });
                Setup(client, $"zone/{i}/stop", new ResponseBase());
                Setup(client, $"zone/{i}/properties", new ZoneAdvancedResponse { uid = i });
            }

            for (int i = 1; i <= Constants.ProgramCount; i++)
            {
                Setup(client, $"program/{i}", new ProgramResponse { uid = i });
                Setup(client, $"program/{i}/start", new ResponseBase { statusCode = 0 });
                Setup(client, $"program/{i}/stop", new ResponseBase { statusCode = 0 });
            }
        }
        private static void Setup<T>(Mock<IRestClient> client, string resource, T data)
        {
            client.Setup(x => x.ExecuteAsync<T>(
It.Is<IRestRequest>(a => a.Resource == resource),
    It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult<IRestResponse<T>>(
                new RestResponse<T>
                {
                    Data = data,
                    StatusCode = HttpStatusCode.OK
                }
                )
          );
        }






        private static List<Zone> _zones
        {
            get
            {
                var ret = new List<Zone>();
                for (int i = 0; i < Constants.ZoneCount; i++)
                    ret.Add(new Zone { uid = i });

                return ret;
            }
        }


        private static List<DailyStat> _dailyStats
        {
            get
            {
                var ret = new List<DailyStat>();
                for (int i = 0; i < Constants.DailyStats; i++)
                    ret.Add(new DailyStat());

                return ret;
            }
        }
        private static List<Program> _programs
        {
            get
            {
                var ret = new List<Program>();
                for (int i = 0; i < Constants.ProgramCount; i++)
                    ret.Add(new Program());

                return ret;
            }
        }
        private static List<PastValue> _wateringHistory
        {
            get
            {
                var ret = new List<PastValue>();
                for (int i = 0; i < Constants.DailyStats; i++)
                    ret.Add(new PastValue());

                return ret;
            }
        }

        private static List<Queue> _queue
        {
            get
            {
                var ret = new List<Queue>();
                for (int i = 0; i < Constants.TestZones.Count; i++)
                    ret.Add(new Queue());

                return ret;
            }
        }

        private static WaterLog _waterLog
        {
            get
            {
                var ret = new WaterLog { days = new List<Day>() };
                for (int i = 0; i < Constants.DailyStats; i++)
                    ret.days.Add(new Day());

                return ret;
            }
        }
    }

}
