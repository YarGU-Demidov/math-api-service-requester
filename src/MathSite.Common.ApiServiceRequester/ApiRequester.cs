using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.Abstractions.Exceptions;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiRequester : IApiRequester
    {
        private readonly IAuthCookieRetriever _authCookieRetriever;
        private readonly IServiceUriBuilder _serviceUriBuilder;

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

            if (!result.IsSuccessStatusCode)
            {
                throw new RequestFailedException(
                    $"Attempt to load URL \"{result.RequestMessage.RequestUri}\" has failed.",
                    result
                );
            }

            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
    }
}