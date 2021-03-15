using Horth.RainMachineNet;
using RainMachineNet.Event;
using RainMachineNet.Model;
using RainMachineNet.Requests;
using RainMachineNet.Responses;
using RainMachineNet.Support;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RainMachineNet
{
    public interface IRainMaker
    {
        Task<bool> LoginAsync(string netNaem, string userId, string pwd);
        Task<DailyStatsResponse> DailyStats();


        Task<ProvisionResponse> GetProvision();
        Task<WifiSettingResponse> GetWifiSettings();
        Task<CloudSettingsResponse> GetCloudSettings();


        Task<WateringResponse> GetWateringStatus();
        Task<ProgramsResponse> GetWateringProgram();
        Task<WateringQueueResponse> GetWateringQueue();

        Task<WaterLogResponse> GetWateringLog(DateTime startDate, DateTime endDate);
        Task<WaterLogResponse> GetWateringLog(DateTime startDate, int days);
        Task<WaterLogResponse> GetWateringLog(int days);
        Task<WaterLogResponse> GetWateringLog();

        Task<WaterLogDetailResponse> GetWateringLogSimulated(DateTime startDate, DateTime endDate);
        Task<WaterLogDetailResponse> GetWateringLogSimulated(DateTime startDate, int days);
        Task<WaterLogDetailResponse> GetWateringLogSimulated(int days);
        Task<WaterLogDetailResponse> GetWateringLogSimulated();

        Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails(DateTime startDate, DateTime endDate);
        Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails(DateTime startDate, int days);
        Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails(int days);
        Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails();

        Task<WaterLogDetailResponse> GetWateringLogDetails(DateTime startDate, DateTime endDate);
        Task<WaterLogDetailResponse> GetWateringLogDetails(DateTime startDate, int days);
        Task<WaterLogDetailResponse> GetWateringLogDetails(int days);
        Task<WaterLogDetailResponse> GetWateringLogDetails();

        Task<WateringHistoryResponse> GetWateringHistory(DateTime startDate, DateTime endDate);
        Task<WateringHistoryResponse> GetWateringHistory(DateTime startDate, int days);
        Task<WateringHistoryResponse> GetWateringHistory(int days);
        Task<WateringHistoryResponse> GetWateringHistory();

        Task<ProgramsResponse> GetAllPrograms();
        Task<ProgramResponse> GetProgram(int id);
        Task<ResponseBase> ProgramStart(int id);
        Task<ResponseBase> ProgramStop(int id);

        Task<ZonesResponse> GetAllZones();
        Task<ZoneResponse> GetZoneProperties(int zoneId);
        Task<ZoneAdvancedResponse> GetZonePropertiesAdvanced(int zoneId);
        Task<SimulationResponse> SimulateZone(SimulateRequest request);
        Task<ZonesResponse> ZoneStart(int zoneId, int minutes);
        Task<ResponseBase> ZoneStop(int zoneId);
        Task<ResponseBase> ZoneStopAll();
        Task<ResponseBase> ZonePauseAll();

        Task<MachineTimeResponse> GetMachineTime();
        Task<UpdateStatusResponse> GetMachineUpdateStatus();

        Task Subscribe(RainMachineNotificationSubscriber<WateringEvent> subscription);
        Task UnSubscribe(RainMachineNotificationSubscriber<WateringEvent> wateringEvent);

        bool IsPolling { get;}
    }


    public class RainMaker : RainMachineLibBase, IRainMaker
    {
        private WateringNotificationProvider wateringEventSubscribers ;
        private readonly System.Timers.Timer _pollingTimer;

        public RainMaker()
        {
            wateringEventSubscribers=new WateringNotificationProvider("");
            _pollingTimer = new System.Timers.Timer(5000);
            _pollingTimer.Elapsed += PollingTimerOnElapsed;
            _pollingTimer.AutoReset = true;
            _pollingTimer.Enabled = true;
        }

        public async Task<bool> LoginAsync(string netName, string userId, string pwd)
        {
            _accessToken = "";
            Log.Information($"LoginAsync({netName},{userId},xxx)");
            base.Initialize(netName);
            try
            {
                _client.Authenticator = new HttpBasicAuthenticator(userId, pwd);
                _account = userId;

                var request = new RestRequest($"auth/login", Method.POST);
                var credentials = new LoginRequest(userId, pwd);

                request.AddParameter("application/json; charset=utf-8", credentials.ToJson(), ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var response = await _client.ExecuteAsync<LoginResponse>(request);
                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var twilioException = new Exception(message, response.ErrorException);
                    throw twilioException;
                }
                _accessToken = response.Data.access_token;
            }
            catch (Exception ex)
            {
                LocalExceptionHandler(ex);
            }

            Log.Information($"LoginAsync({netName},{userId},xxx) => {_accessToken}");
            return !string.IsNullOrWhiteSpace(_accessToken);
        }


        public async Task<DailyStatsResponse> DailyStats()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"dailystats", Method.GET);
            var response = await Execute<DailyStatsResponse>(request);
            return response;
        }

        #region Provision
        public async Task<ProvisionResponse> GetProvision()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"provision", Method.GET);
            var response = await Execute<ProvisionResponse>(request);
            return response;
        }
        public async Task<WifiSettingResponse> GetWifiSettings()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"provision/wifi", Method.GET);
            var response = await Execute<WifiSettingResponse>(request);
            return response;
        }
        public async Task<CloudSettingsResponse> GetCloudSettings()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"provision/cloud", Method.GET);
            var response = await Execute<CloudSettingsResponse>(request);
            return response;
        }
        #endregion

        #region Zones
        public async Task<ZonesResponse> GetAllZones()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"zone", Method.GET);
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        public async Task<ZoneResponse> GetZoneProperties(int zoneId)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"zone/{zoneId}", Method.GET);
            var response = await Execute<ZoneResponse>(request);
            return response;
        }
        public async Task<ZoneAdvancedResponse> GetZonePropertiesAdvanced(int zoneId)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"zone/{zoneId}/properties", Method.GET);
            var response = await Execute<ZoneAdvancedResponse>(request);
            return response;
        }
        public async Task<SimulationResponse> SimulateZone(SimulateRequest request)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var response = await Execute<SimulationResponse>($"zone/simulate", request);
            return response;
        }
        public async Task<ZonesResponse> ZoneStart(int zoneId, int minutes)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var zone = new ZoneRequest { time = minutes };
            var request = new RestRequest($"zone/{zoneId}/start", Method.POST);
            request.AddParameter("application/json; charset=utf-8", zone.ToJson(), ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        public async Task<ResponseBase> ZoneStop(int zoneId)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"zone/{zoneId}/stop", Method.POST);
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        public async Task<ResponseBase> ZoneStopAll()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"zone/stopall", Method.POST);
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        public async Task<ResponseBase> ZonePauseAll()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"zone/pauseall", Method.POST);
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        #endregion

        #region Watering
        public async Task<WateringResponse> GetWateringStatus()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/zone", Method.GET);
            var response = await Execute<WateringResponse>(request);
            return response;
        }

        public async Task<ProgramsResponse> GetWateringProgram()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/program", Method.GET);
            var response = await Execute<ProgramsResponse>(request);
            return response;
        }

        public async Task<WateringQueueResponse> GetWateringQueue()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/queue", Method.GET);
            var response = await Execute<WateringQueueResponse>(request);
            return response;
        }

        public async Task<WaterLogResponse> GetWateringLog(DateTime startDate, int days)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/past/{startDate.ToString("yyyy-MM-dd")}/{days}", Method.GET);
            var response = await Execute<WaterLogResponse>(request);
            return response;
        }
        public async Task<WaterLogResponse> GetWateringLog(DateTime startDate, DateTime endDate)
        {
            var date = startDate;
            if (startDate > endDate)
                date = endDate;
            return await GetWateringLog(date, (int)Math.Abs((startDate - endDate).TotalDays));
        }
        public async Task<WaterLogResponse> GetWateringLog(int days)
        {
            var date = DateTime.Now.AddDays(-(days - 1));
            return await GetWateringLog(date, days - 1);
        }
        public async Task<WaterLogResponse> GetWateringLog()
        {
            var date = DateTime.Now;
            return await GetWateringLog(date, 0);
        }


        public async Task<WateringHistoryResponse> GetWateringHistory(DateTime startDate, int days)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/past/{startDate.ToString("yyyy-MM-dd")}/{days}", Method.GET);
            var response = await Execute<WateringHistoryResponse>(request);
            return response;
        }
        public async Task<WateringHistoryResponse> GetWateringHistory(DateTime startDate, DateTime endDate)
        {
            var date = startDate;
            if (startDate > endDate)
                date = endDate;
            return await GetWateringHistory(date, (int)Math.Abs((startDate - endDate).TotalDays));
        }
        public async Task<WateringHistoryResponse> GetWateringHistory(int days)
        {
            var date = DateTime.Now.AddDays(-(days - 1));
            return await GetWateringHistory(date, days - 1);
        }
        public async Task<WateringHistoryResponse> GetWateringHistory()
        {
            var date = DateTime.Now;
            return await GetWateringHistory(date, 0);
        }


        public async Task<WaterLogDetailResponse> GetWateringLogDetails(DateTime startDate, int days)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/log/details/{startDate.ToString("yyyy-MM-dd")}/{days}", Method.GET);
            var response = await Execute<WaterLogDetailResponse>(request);
            return response;
        }
        public async Task<WaterLogDetailResponse> GetWateringLogDetails(DateTime startDate, DateTime endDate)
        {
            var date = startDate;
            if (startDate > endDate)
                date = endDate;
            return await GetWateringLogDetails(date, (int)Math.Abs((startDate - endDate).TotalDays));
        }
        public async Task<WaterLogDetailResponse> GetWateringLogDetails(int days)
        {
            var date = DateTime.Now.AddDays(-days);
            return await GetWateringLogDetails(date, days);
        }
        public async Task<WaterLogDetailResponse> GetWateringLogDetails()
        {
            var date = DateTime.Now;
            return await GetWateringLogDetails(date, 0);
        }


        public async Task<WaterLogDetailResponse> GetWateringLogSimulated(DateTime startDate, int days)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/log/simulated/{startDate.ToString("yyyy-MM-dd")}/{days}", Method.GET);
            var response = await Execute<WaterLogDetailResponse>(request);
            return response;
        }
        public async Task<WaterLogDetailResponse> GetWateringLogSimulated(DateTime startDate, DateTime endDate)
        {
            var date = startDate;
            if (startDate > endDate)
                date = endDate;
            return await GetWateringLogSimulated(date, (int)Math.Abs((startDate - endDate).TotalDays));
        }
        public async Task<WaterLogDetailResponse> GetWateringLogSimulated(int days)
        {
            var date = DateTime.Now.AddDays(-days);
            return await GetWateringLogSimulated(date, days);
        }
        public async Task<WaterLogDetailResponse> GetWateringLogSimulated()
        {
            var date = DateTime.Now;
            return await GetWateringLogSimulated(date, 0);
        }


        public async Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails(DateTime startDate, int days)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"watering/log/simulated/details/{startDate.ToString("yyyy-MM-dd")}/{days}", Method.GET);
            var response = await Execute<WaterLogDetailResponse>(request);
            return response;
        }
        public async Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails(DateTime startDate, DateTime endDate)
        {
            var date = startDate;
            if (startDate > endDate)
                date = endDate;
            return await GetWateringLogSimulatedDetails(date, (int)Math.Abs((startDate - endDate).TotalDays));
        }
        public async Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails(int days)
        {
            var date = DateTime.Now.AddDays(-days);
            return await GetWateringLogSimulatedDetails(date, days);
        }
        public async Task<WaterLogDetailResponse> GetWateringLogSimulatedDetails()
        {
            var date = DateTime.Now;
            return await GetWateringLogSimulatedDetails(date, 0);
        }

        #endregion

        #region Program
        public async Task<ProgramsResponse> GetAllPrograms()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"program", Method.GET);
            var response = await Execute<ProgramsResponse>(request);
            return response;
        }
        public async Task<ProgramResponse> GetProgram(int id)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"program/{id}", Method.GET);
            var response = await Execute<ProgramResponse>(request);
            return response;
        }
        public async Task<ResponseBase> ProgramStart(int programId)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"program/{programId}/start", Method.POST);
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        public async Task<ResponseBase> ProgramStop(int programId)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"program/{programId}/stop", Method.POST);
            var response = await Execute<ZonesResponse>(request);
            return response;
        }
        #endregion

        #region Machine
        public async Task<MachineTimeResponse> GetMachineTime()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"machine/time", Method.GET);
            var response = await Execute<MachineTimeResponse>(request);
            return response;
        }
        public async Task<UpdateStatusResponse> GetMachineUpdateStatus()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
                throw new RainMakerAuthenicationException();

            var request = new RestRequest($"machine/update", Method.GET);
            var response = await Execute<UpdateStatusResponse>(request);
            return response;
        }

        #endregion

        public async Task Subscribe(RainMachineNotificationSubscriber<WateringEvent> subscription)
        {
            await wateringEventSubscribers.SubscribeAsync(subscription);
        }

        public async Task UnSubscribe(RainMachineNotificationSubscriber<WateringEvent> wateringEvent)
        {
            await wateringEventSubscribers.UnSubscribeAsync(wateringEvent);
        }

        public bool IsPolling=> wateringEventSubscribers.HasSubscribers() && _pollingTimer.Enabled;

        private async void PollingTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _pollingTimer.Enabled = false;

                if(!wateringEventSubscribers.HasSubscribers())
                    return;

                if (string.IsNullOrWhiteSpace(_accessToken))
                    throw new RainMakerAuthenicationException();

                var watering=await GetWateringStatus();
                wateringEventSubscribers.EventNotification(watering);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "CheckSpotifyTimerOnElapsed");
            }
            finally
            {
                _pollingTimer.Enabled = true;
            }
        }
    }
}
