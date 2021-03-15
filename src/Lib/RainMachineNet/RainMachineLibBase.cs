using RainMachineNet.Requests;
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
        protected const string DeviceCertId = "0D662453BAC4F5B5E874B4341B9E22B3270336CC";

        public RainMachineLibBase()
        {
        }
        public void Initialize(string netName)
        {
            _client = new RestClient(string.Format(BaseUrl, netName));
            _client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                var thumbprint = cert.GetCertHashString();

                if (thumbprint == DeviceCertId)
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
        }
        public async Task<T> Execute<T>(string endPoint, RequestBase request) where T : new()
        {
            var restRequest = new RestRequest(endPoint, Method.POST);
            restRequest.AddParameter("application/json; charset=utf-8", request.ToJson(), ParameterType.RequestBody);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            return await Execute<T>(restRequest);
        }

        public async Task<T> Execute<T>(RestRequest request) where T : new()
        {
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
            return response.Data;
        }

        protected void LocalExceptionHandler(Exception ex, string msg = "")
        {
            Log.Error($"{msg}", ex);
        }
    }
}
