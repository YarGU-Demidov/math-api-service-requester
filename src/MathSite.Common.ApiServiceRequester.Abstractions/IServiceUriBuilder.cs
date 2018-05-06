using System;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IServiceUriBuilder
    {
        Uri FromPath(string path, ApiEndpointConfiguration endpointConfiguration);
    }
}