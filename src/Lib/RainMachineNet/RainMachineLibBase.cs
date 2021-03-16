using RainMachineNet.Requests;
<<<<<<< HEAD
using RainMachineNet.Support;
=======
>>>>>>> Add project files.
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
<<<<<<< HEAD
=======
        protected const string DeviceCertId = "0D662453BAC4F5B5E874B4341B9E22B3270336CC";
>>>>>>> Add project files.

        public RainMachineLibBase()
        {
        }
<<<<<<< HEAD
        public void Initialize(string netName, string deviceCertId)
        {
            Log.Debug($"Initialize({netName}) Rest client to communicate with local device");
=======
        public void Initialize(string netName)
        {
>>>>>>> Add project files.
            _client = new RestClient(string.Format(BaseUrl, netName));
            _client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                var thumbprint = cert.GetCertHashString();

<<<<<<< HEAD
                if (thumbprint == deviceCertId)
=======
                if (thumbprint == DeviceCertId)
>>>>>>> Add project files.
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
<<<<<<< HEAD
            Log.Debug($"Initialize({netName}) complete");
        }
        public async Task<T> Execute<T>(string endPoint, RequestBase request) where T : new()
        {
            Log.Debug($"Execute({endPoint}) helper");
=======
        }
        public async Task<T> Execute<T>(string endPoint, RequestBase request) where T : new()
        {
>>>>>>> Add project files.
            var restRequest = new RestRequest(endPoint, Method.POST);
            restRequest.AddParameter("application/json; charset=utf-8", request.ToJson(), ParameterType.RequestBody);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            return await Execute<T>(restRequest);
        }

        public async Task<T> Execute<T>(RestRequest request) where T : new()
        {
<<<<<<< HEAD
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
=======
            if (!string.IsNullOrEmpty(_accessToken))
                request.AddQueryParameter("access_token", _accessToken); // used on every request but login

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var response = await _client.ExecuteAsync<T>(request);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new Exception(message, response.ErrorException);
                throw twilioException;
            }
>>>>>>> Add project files.
            return response.Data;
        }

        protected void LocalExceptionHandler(Exception ex, string msg = "")
        {
            Log.Error($"{msg}", ex);
        }
    }
}
