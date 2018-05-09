using System.Net;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiRequester : IApiRequester
    {
        private readonly IServiceUriBuilder _serviceUriBuilder;
        private readonly IAuthCookieRetriever _authCookieRetriever;

        public ApiRequester(
            IServiceUriBuilder serviceUriBuilder,
            IAuthCookieRetriever authCookieRetriever
        )
        {
            _serviceUriBuilder = serviceUriBuilder;
            _authCookieRetriever = authCookieRetriever;
        }

        public virtual async Task<T> GetAsync<T>(IApiEndpoint endpoint, string path)
        {
            var authCookie = _authCookieRetriever.GetAuthCookie();
            var result = await endpoint.GetAsync(path, authCookie, _serviceUriBuilder);

            return !result.IsSuccessStatusCode
                ? default
                : JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
    }
}