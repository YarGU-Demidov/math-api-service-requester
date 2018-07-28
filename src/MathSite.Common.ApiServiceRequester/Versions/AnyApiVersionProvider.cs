using MathSite.Common.ApiServiceRequester.Abstractions;

namespace MathSite.Common.ApiServiceRequester.Versions
{
    public class AnyApiVersionProvider : IApiVersionProvider
    {
        public string GetVersion()
        {
            return null;
        }
    }
}