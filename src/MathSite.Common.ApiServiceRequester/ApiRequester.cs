using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        
        public async Task<T> GetAsync<T>(ServiceMethod serviceMethod, MethodArgs args = null)
        {
            var authData = _authDataRetriever.GetAuthData();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);
            
            var result = await endpoint.GetAsync(
                BuildUrlWithParams(serviceMethod.MethodName, args), 
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

        public async Task<T> PostAsync<T>(ServiceMethod serviceMethod, MethodArgs args = null, IDictionary<string, IEnumerable<Stream>> files = null)
        {
            var authData = _authDataRetriever.GetAuthData();
            var endpoint = _apiEndpointFactory.GetEndpoint(serviceMethod);

            #region надо тестить!

            var content = new MultipartFormDataContent
            {
                new FormUrlEncodedContent(args ?? new MethodArgs())
            };

            foreach (var filesPair in files ?? new Dictionary<string, IEnumerable<Stream>>())
            {
                var filesData = new MultipartContent();
                filesPair.Value
                    .ToList()
                    .ForEach(stream =>
                    {
                        var streamContent = new StreamContent(stream);
                        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        filesData.Add(streamContent);
                    });

                filesData.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                filesData.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                content.Add(filesData, filesPair.Key);
            }

            #endregion
            
            var result = await endpoint.PostAsync(
                serviceMethod.MethodName, 
                content,
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