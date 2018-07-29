using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.Abstractions.Exceptions;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiRequester : IApiRequester
    {
        private readonly IAuthCookieRetriever _authCookieRetriever;
        private readonly IApiEndpointFactory _apiEndpointFactory;
        private readonly IServiceUriBuilder _serviceUriBuilder;

        public ApiRequester(
            IServiceUriBuilder serviceUriBuilder,
            IAuthCookieRetriever authCookieRetriever,
            IApiEndpointFactory apiEndpointFactory
        )
        {
            _serviceUriBuilder = serviceUriBuilder;
            _authCookieRetriever = authCookieRetriever;
            _apiEndpointFactory = apiEndpointFactory;
        }

        public async Task<T> GetAsync<T>(ServiceMethod serviceMethod, Dictionary<string, string> data)
        {
            var authCookie = _authCookieRetriever.GetAuthCookie();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);
            
            var result = await endpoint.GetAsync(
                BuildUrlWithParams(serviceMethod.MethodName, data), 
                authCookie, 
                _serviceUriBuilder
            );
            
            if (!result.IsSuccessStatusCode)
            {
                throw new RequestFailedException(
                    $"Attempt to load URL \"{result.RequestMessage.RequestUri}\" has failed.",
                    result
                );
            }

            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }

        public async Task<T> PostAsync<T>(ServiceMethod serviceMethod, Dictionary<string, string> data)
        {
            var authCookie = _authCookieRetriever.GetAuthCookie();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);
            var result = await endpoint.PostAsync(serviceMethod.MethodName, new FormUrlEncodedContent(data ?? new Dictionary<string, string>()), authCookie, _serviceUriBuilder);
          
            if (!result.IsSuccessStatusCode)
            {
                throw new RequestFailedException(
                    $"Attempt to load URL \"{result.RequestMessage.RequestUri}\" has failed.",
                    result
                );
            }

            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }


        private string BuildUrlWithParams(string url, Dictionary<string, string> data)
        {
            if (data == null || data.Count == 0)
                return url;

            return url + "?" + data.Select((pair) => $"{pair.Key}={pair.Value}").Aggregate((f, s) => $"{f}&{s}");
        }
    }
}