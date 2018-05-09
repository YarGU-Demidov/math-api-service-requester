using System.Net;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace ApiServiceRequesterTests
{
    public class TestCookieRetriever : IAuthCookieRetriever
    {
        private readonly Cookie _cookie;

        public TestCookieRetriever(Cookie cookie)
        {
            _cookie = cookie;
        }

        public Cookie GetAuthCookie()
        {
            return _cookie;
        }
    }
}