using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiEndpoint : IApiEndpoint
    {
        private ApiEndpointConfiguration Configuration { get; }

        private readonly HttpClient _client;
        private readonly HttpClientHandler _clientHandler;

        public ApiEndpoint(ApiEndpointConfiguration configuration)
        {
            Configuration = configuration;
            _clientHandler = new HttpClientHandler();
            _client = new HttpClient(_clientHandler);
        }

        public virtual async Task<HttpResponseMessage> GetAsync(
            string path,
            Cookie authCookie,
            IServiceUriBuilder serviceUriBuilder
        )
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(path);

            SetCookie(authCookie);

            return await _client.GetAsync(
                serviceUriBuilder.FromPath(path, Configuration)
            );
        }

        public virtual async Task<HttpResponseMessage> PostAsync(
            string path,
            HttpContent data,
            Cookie authCookie,
            IServiceUriBuilder serviceUriBuilder
        )
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(path);

            SetCookie(authCookie);

            return await _client.PostAsync(
                serviceUriBuilder.FromPath(path, Configuration),
                data
            );
        }

        private void SetCookie(Cookie authCookie)
        {
            if (authCookie == null)
            {
                return;
            }

            _clientHandler.CookieContainer.Add(authCookie);
        }
    }
}