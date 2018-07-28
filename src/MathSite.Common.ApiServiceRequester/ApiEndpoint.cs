using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.Versions;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiEndpoint : IApiEndpoint
    {
        private readonly IApiVersionProvider _apiVersionProvider;
        private readonly HttpClient _client;
        private readonly HttpClientHandler _clientHandler;

        public ApiEndpoint(ApiEndpointConfiguration configuration)
            : this(configuration, new AnyApiVersionProvider())
        {
        }

        public ApiEndpoint(ApiEndpointConfiguration configuration, IApiVersionProvider apiVersionProvider)
        {
            _apiVersionProvider = apiVersionProvider;
            Configuration = configuration;
            _clientHandler = new HttpClientHandler();
            _client = new HttpClient(_clientHandler);
        }

        private ApiEndpointConfiguration Configuration { get; }

        public virtual async Task<HttpResponseMessage> GetAsync(
            string path,
            Cookie authCookie,
            IServiceUriBuilder serviceUriBuilder
        )
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(path);

            SetCookie(authCookie);

            var uri = serviceUriBuilder.FromPath(path, Configuration, _apiVersionProvider);

            return await _client.GetAsync(uri);
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
                serviceUriBuilder.FromPath(path, Configuration, _apiVersionProvider),
                data
            );
        }

        private void SetCookie(Cookie authCookie)
        {
            if (authCookie == null) return;

            _clientHandler.CookieContainer.Add(authCookie);
        }
    }
}