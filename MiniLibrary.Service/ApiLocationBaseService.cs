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
    public class ApiLocationBaseService
    {
        private RestClient _client = new RestClient(Settings.Get().ApiLocationHost);

        protected RestClient Client => _client;

        protected async Task<T> GetAsync<T>(string resource) where T : class
        {
            return await GetAsync<T>(resource, null);
        }
        protected async Task<T> GetAsync<T>(string resource, object? body) where T : class
        {
            return await GetResponseAsync<T>(resource, body, Method.Get);
        }

        protected async Task<T> PostAsync<T>(string resource, object? body) where T : class
        {
            return await GetResponseAsync<T>(resource, body, Method.Post);
        }

        private async Task<T> GetResponseAsync<T>(string resource, object? body, Method method) where T : class
        {
            RestRequest request = new RestRequest(resource, method);
            if (body != null)
                request.AddJsonBody(body, ContentType.Json);
            return await GetResponseDataAsync<T>(request);
        }

        private async Task<T> GetResponseDataAsync<T>(RestRequest request) where T : class
        {
            RestResponse response = await Client.ExecuteAsync(request);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.NotModified:
                    return JsonConvert.DeserializeObject<T>(response.Content);
                case System.Net.HttpStatusCode.NotFound:
                case System.Net.HttpStatusCode.BadRequest:
                    return null;
                case System.Net.HttpStatusCode.InternalServerError:
                default:
                    throw new ApplicationException("Error executing the request.");
            }
        }
    }
}
