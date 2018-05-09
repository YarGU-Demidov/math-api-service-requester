using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester.UriBuilders
{
    public class AfterDomainServiceUriBuilder : IServiceUriBuilder
    {
        private readonly AuthConfig _authConfig;

        public AfterDomainServiceUriBuilder(IOptions<AuthConfig> options)
        {
            _authConfig = options.Value;
        }

        public Uri FromPath(string path, ApiEndpointConfiguration endpointConfiguration)
        {
            var uriBuilder = new UriBuilder(_authConfig.SiteUrl)
            {
                Scheme = _authConfig.UseHttps ? "https" : "http",
                Path = $"services/{endpointConfiguration.EndpointAlias}/{path}"
            };

            return uriBuilder.Uri;
        }
    }
}