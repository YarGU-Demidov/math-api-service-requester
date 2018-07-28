using System.Net;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MathSite.Common.ApiServiceRequester
{
    public class AuthCookieRetriever : IAuthCookieRetriever
    {
        private string _authCookieKey = "Authorization";

        public AuthCookieRetriever(IHttpContextAccessor contextAccessor, IOptions<AuthConfig> options)
        {
            Context = contextAccessor.HttpContext;
            AuthConfig = options.Value;
        }

        private HttpContext Context { get; }
        private AuthConfig AuthConfig { get; }

        public Cookie GetAuthCookie()
        {
            var cookies = Context.Request.Cookies;

            var hasAuthCookie = cookies.ContainsKey(_authCookieKey);

            if (!hasAuthCookie)
                return null;

            var coockieValue = cookies[_authCookieKey];
            var siteName = AuthConfig.SiteUrl;

            return new Cookie(_authCookieKey, coockieValue, "/", siteName) {HttpOnly = true};
        }

        public void SetCookieKey(string key)
        {
            _authCookieKey = key;
        }
    }
}