using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MathSite.Common.ApiServiceRequester
{
    public class AuthDataRetriever : IAuthDataRetriever
    {
        private string _authCookieKey = "Authorization";

        public AuthDataRetriever(IHttpContextAccessor contextAccessor, IOptions<AuthConfig> options)
        {
            Context = contextAccessor.HttpContext;
            AuthConfig = options.Value;
        }

        private HttpContext Context { get; }
        private AuthConfig AuthConfig { get; }

        public AuthData GetAuthData()
        {
            var cookies = Context.Request.Cookies;

            var hasAuthCookie = cookies.ContainsKey(_authCookieKey);

            if (!hasAuthCookie)
                return null;

            var coockieValue = cookies[_authCookieKey];

            return new AuthData
            {
                CookieDomain = AuthConfig.SiteUrl,
                CookieKey = _authCookieKey,
                CookiePath = "/",
                CookieValue = coockieValue
            };
        }
    }
}