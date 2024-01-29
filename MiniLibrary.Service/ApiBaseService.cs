using MiniLibrary.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniLibrary.Service
{
    public class ApiBaseService
    {
        private RestClient _client = new RestClient(Settings.Get().ApiHost);
        private string _apiKey = Settings.Get().ApiKey;

        protected RestClient Client  => _client;

        protected async Task<T> GetAsync<T>(string resource) where T : class
        {
            RestRequest request = new RestRequest(resource, Method.Get);
            return await GetResponseDataAsync<T>(request);
        }

        protected async Task<T> PostAsync<T>(string resource, object body) where T : class
        {
            RestRequest request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(body, ContentType.Json);
            return await GetResponseDataAsync<T>(request);
        }

        private async Task<T> GetResponseDataAsync<T>(RestRequest request) where T : class
        {
            request.AddHeader("API-Key", _apiKey);
            RestResponse response = await Client.ExecuteAsync(request);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Created:
                case System.Net.HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<T>(response.Content);
                case System.Net.HttpStatusCode.BadRequest:
                    string errorMessage = string.Empty;
                    if (response.Content.Contains("errors"))
                    {
                        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(response.Content);
                        errorMessage = jsonObject["errors"].First().First().First().Value<string>();
                    }
                    else
                        errorMessage = response.Content;
                    throw new ApplicationException(errorMessage);
                case System.Net.HttpStatusCode.InternalServerError:
                default:
                    throw new ApplicationException("Error executing the request.");
            }
        }
    }
}
