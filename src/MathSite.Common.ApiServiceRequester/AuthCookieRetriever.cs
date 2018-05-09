using System.Net;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MathSite.Common.ApiServiceRequester
{
    public class AuthCookieRetriever : IAuthCookieRetriever
    {
        public AuthCookieRetriever(IHttpContextAccessor contextAccessor, IOptions<AuthConfig> options)
        {
            Context = contextAccessor.HttpContext;
            AuthConfig = options.Value;
        }

        private HttpContext Context { get; }
        private AuthConfig AuthConfig { get; }

        public Cookie GetAuthCookie()
        {
            const string authCookieKey = "Authorization";

            var cookies = Context.Request.Cookies;

            var hasAuthCookie = cookies.ContainsKey(authCookieKey);

            if (!hasAuthCookie)
                return null;

            var coockieValue = cookies[authCookieKey];
            var siteName = AuthConfig.SiteUrl;

            return new Cookie(authCookieKey, coockieValue, "/", siteName) {HttpOnly = true};
        }
    }
}