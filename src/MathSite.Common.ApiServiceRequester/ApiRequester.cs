using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.Abstractions.Exceptions;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiRequester : IApiRequester
    {
        private readonly IAuthDataRetriever _authDataRetriever;
        private readonly IApiEndpointFactory _apiEndpointFactory;
        private readonly IServiceUriBuilder _serviceUriBuilder;

        public ApiRequester(
            IServiceUriBuilder serviceUriBuilder,
            IAuthDataRetriever authDataRetriever,
            IApiEndpointFactory apiEndpointFactory
        )
        {
            _serviceUriBuilder = serviceUriBuilder;
            _authDataRetriever = authDataRetriever;
            _apiEndpointFactory = apiEndpointFactory;
        }

        public async Task<T> GetAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, string>> data = null)
        {
            var authData = _authDataRetriever.GetAuthData();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);
            
            var result = await endpoint.GetAsync(
                BuildUrlWithParams(serviceMethod.MethodName, data), 
                GetCookieFromAuthData(authData),
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

        public async Task<T> PostAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, string>> data = null)
        {
            var authData = _authDataRetriever.GetAuthData();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);
            
            var result = await endpoint.PostAsync(
                serviceMethod.MethodName, 
                new FormUrlEncodedContent(data ?? new Dictionary<string, string>()),
                GetCookieFromAuthData(authData), 
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

        public Task<T> GetAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, IEnumerable<string>>> data)
        {
            return GetAsync<T>(serviceMethod, ConvertEnumerableValueToString(data));
        }

        public Task<T> PostAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, IEnumerable<string>>> data)
        {
            return PostAsync<T>(serviceMethod, ConvertEnumerableValueToString(data));
        }

        public async Task<T> SendDataAsync<T>(ServiceMethod serviceMethod, Stream dataStream)
        {
            var authData = _authDataRetriever.GetAuthData();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);
            
            var result = await endpoint.PostAsync(
                serviceMethod.MethodName, 
                new StreamContent(dataStream),
                GetCookieFromAuthData(authData), 
                _serviceUriBuilder
            );
          
            if (!result.IsSuccessStatusCode)
            {
                throw new RequestFailedException(
                    $"Attempt to upload file to URL \"{result.RequestMessage.RequestUri}\" has failed.",
                    result
                );
            }

            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }

        private IEnumerable<KeyValuePair<string, string>> ConvertEnumerableValueToString(IEnumerable<KeyValuePair<string, IEnumerable<string>>> data)
        {
            var updatedValues = new List<KeyValuePair<string, string>>();

            foreach (var pair in data)
            {
                if (pair.Value == null)
                    continue;

                updatedValues.AddRange(
                    pair.Value.Select(val => new KeyValuePair<string, string>(pair.Key, val))
                );
            }

            return updatedValues;
        }


        private string BuildUrlWithParams(string url, IEnumerable<KeyValuePair<string, string>> incomingData)
        {
            var data = incomingData == null 
                ? new List<KeyValuePair<string, string>>() 
                : incomingData.ToList();

            if (!data.Any())
                return url;

            return url + "?" + data.Select(pair => $"{pair.Key}={pair.Value}").Aggregate((f, s) => $"{f}&{s}");
        }

        private Cookie GetCookieFromAuthData(AuthData authData)
        {
            if (authData == null)
                return null;

            return new Cookie 
            {
                Name = authData.CookieKey, 
                Value = authData.CookieValue, 
                Path = authData.CookiePath, 
                Domain = authData.CookieDomain,
                HttpOnly = true
            };
        }
    }
}