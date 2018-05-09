using System.Net;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IAuthCookieRetriever
    {
        Cookie GetAuthCookie();
    }
}