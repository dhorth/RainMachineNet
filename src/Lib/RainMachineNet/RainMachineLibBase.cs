using RainMachineNet.Requests;
using RainMachineNet.Responses;
using RainMachineNet.Support;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;

namespace Horth.RainMachineNet
{
    public class RainMachineLibBase
    {
        protected IRestClient _client;
        protected string _account;
        protected string _accessToken;

        protected const string BaseUrl = "https://{0}:8080/api/4/";
        public RainMachineLibBase()
        {
        }
        public void Initialize(string netName, string deviceCertId)
        {
            Log.Debug($"Initialize({netName}) Rest client to communicate with local device");
            if(_client!=null)
            {
                Log.Information($"Already have a restclient skipping initialization");
                return;
            }
            _client = new RestClient(string.Format(BaseUrl, netName));
            _client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                var thumbprint = cert.GetCertHashString();

                if (thumbprint == deviceCertId)
                {
                    Log.Information($"Allowing Device Cert ");
                    return true;
                }
                else
                {
                    Log.Error($"SSL Certificate thumbprint is unknown {thumbprint}");
                    return true;
                };
            };
            Log.Debug($"Initialize({netName}) complete");
        }
        public void UnitTestInitialize(IRestClient client)
        {
            Log.Debug($"UnitTestInitialize({client.BaseUrl}) Rest client to communicate with local device");
            _client=client;
            Log.Debug($"UnitTestInitialize({client.BaseUrl}) complete");
        }
        public async Task<T> Execute<T>(string endPoint, RequestBase request) where T : IResponseBase, new()
        {
            Log.Debug($"Execute({endPoint}) helper");
            var restRequest = new RestRequest(endPoint, Method.POST);
            restRequest.AddParameter("application/json; charset=utf-8", request.ToJson(), ParameterType.RequestBody);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            return await Execute<T>(restRequest);
        }

        public async Task<T> Execute<T>(RestRequest request) where T : IResponseBase, new()
        {
            Log.Debug($"Execute Request({request.Resource}) ");
            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.AddQueryParameter("access_token", _accessToken); // used on every request but login
            }
            else
            {
                Log.Warning("Request being made without access token!");
            }
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = await _client.ExecuteAsync<T>(request);
            if (response.ErrorException != null)
            {
                var twilioException = new RainMakerExecuteException(!string.IsNullOrEmpty(_accessToken), response.ErrorException);
                throw twilioException;
            }
            Log.Debug($"Execute Request({request.Resource}) => {response.Data!=null} ");
            return response.Data;
        }

        protected void LocalExceptionHandler(Exception ex, string msg = "")
        {
            Log.Error($"{msg}", ex);
        }
    }
}
