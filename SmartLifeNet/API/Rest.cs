using Newtonsoft.Json;
using RestSharp;
using SmartLifeNet.Classes;
using SmartLifeNet.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartLifeNet.API
{

    public static class Rest
    {
        public static async Task<string> GetCredentials(string email, string password, string region)
        {
            var host = Constants.URLs.GetHost(region);
            var url = Constants.URLs.GetAuthUrl(host);
            var client = new RestClient(url) {Timeout = -1};

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Host", host);
            request.AddParameter("userName", email);
            request.AddParameter("password", password);
            request.AddParameter("countryCode", region);
            request.AddParameter("bizType", Constants.AppData.BIZ_TYPE);
            request.AddParameter("from", Constants.AppData.FROM);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }

        public static async Task<string> GetDevices(string region, string accessToken)
        {
            var host = Constants.URLs.GetHost(region);
            var url = Constants.URLs.GetSkillUrl(host);
            var client = new RestClient(url) {Timeout = -1};
           
            var request = new RestRequest(Method.POST);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept-Language", "es-ES,es;q=0.9,en;q=0.8");

            var body = new {
                header = new {
                    name = "discovery",
                    @namespace = "discovery",
                    payloadVersion = 1,
                },
                payload = new { accessToken }
            };

            request.AddParameter("application/json", body.AsJson(), ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }

        public static async Task<string> SetDeviceSkill(string region, string deviceId, string accessToken, int state)
        {
            var host = Constants.URLs.GetHost(region);
            var url = Constants.URLs.GetSkillUrl(host);
            var client = new RestClient(url) {Timeout = -1};
           
            var request = new RestRequest(Method.POST);
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept-Language", "es-ES,es;q=0.9,en;q=0.8");
    
            var body = new {
                header = new {
                    name = "turnOnOff",
                    @namespace = "control",
                    payloadVersion = 1,
                },
                payload = new { accessToken, devId = deviceId, value = state  }
            };

            request.AddParameter("application/json", body.AsJson(), ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);
            return response.Content;
        }

    }
}
