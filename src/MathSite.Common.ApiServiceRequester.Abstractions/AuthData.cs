using System.Net;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public class AuthData
    {
        public string CookieDomain { get; set; }
        public string CookieKey { get;set; }
        public string CookieValue { get; set; }
        public string CookiePath { get; set; }
    }
}