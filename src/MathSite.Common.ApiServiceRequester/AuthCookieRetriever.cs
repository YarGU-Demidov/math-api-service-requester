using System.Net;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MathSite.Common.ApiServiceRequester
{
    public class AuthCookieRetriever : IAuthCookieRetriever
    {
        private const string AuthCookieKey = "Authorization";

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

            var hasAuthCookie = cookies.ContainsKey(AuthCookieKey);

            if (!hasAuthCookie)
                return null;

            var coockieValue = cookies[AuthCookieKey];
            var siteName = AuthConfig.SiteUrl;

            return new Cookie(AuthCookieKey, coockieValue, "/", siteName) {HttpOnly = true};
        }
    }
}