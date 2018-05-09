using System;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace MathSite.Common.ApiServiceRequester.UriBuilders
{
    public class ExactUrlServiceUrlBuilder : IServiceUriBuilder
    {
        public Uri FromPath(string path, ApiEndpointConfiguration endpointConfiguration)
        {
            if (string.IsNullOrWhiteSpace(endpointConfiguration.EndpointAddress))
                throw new ArgumentNullException(endpointConfiguration.EndpointAddress);
            
            var currentPath = new Uri(endpointConfiguration.EndpointAddress).PathAndQuery ?? "";

            var newPath = $"{currentPath}/{path}";
            
            return new Uri(new Uri(endpointConfiguration.EndpointAddress), newPath);
        }
    }
}