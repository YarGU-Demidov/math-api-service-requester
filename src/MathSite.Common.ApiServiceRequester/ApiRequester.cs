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
        private readonly IConfiguration _configuration;
        private readonly IServiceUriBuilder _serviceUriBuilder;
        public HttpContext Context { get; }

        public ApiRequester(
            IHttpContextAccessor contextAccessor, 
            IConfiguration configuration, 
            IServiceUriBuilder serviceUriBuilder
        )
        {
            _configuration = configuration;
            _serviceUriBuilder = serviceUriBuilder;
            Context = contextAccessor.HttpContext;
        }

        public virtual async Task<T> GetAsync<T>(IApiEndpoint endpoint, string path)
            where T : class
        {
            var authCookie = GetAuthCookie();
            var result = await endpoint.GetAsync(path, authCookie, _serviceUriBuilder);

            return !result.IsSuccessStatusCode
                ? null
                : JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }

        private Cookie GetAuthCookie()
        {
            const string authCookieKey = "Authorization";
            
            var cookies = Context.Request.Cookies;
            
            var hasAuthCookie = cookies.ContainsKey(authCookieKey);

            if (!hasAuthCookie)
                return null;

            var coockieValue = cookies[authCookieKey];
            var siteName = _configuration[ConfigurationConstants.SiteUrlKey];

            return new Cookie(authCookieKey, coockieValue, "/", siteName) {HttpOnly = true};
        }
    }
}