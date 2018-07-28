using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.Extensions.Options;

namespace MathSite.Common.ApiServiceRequester.UriBuilders
{
    public class AfterDomainServiceUriBuilder : IServiceUriBuilder
    {
        private readonly AuthConfig _authConfig;

        public AfterDomainServiceUriBuilder(IOptions<AuthConfig> options)
        {
            _authConfig = options.Value;
        }

        public Uri FromPath(string path, ApiEndpointConfiguration endpointConfiguration, IApiVersionProvider apiVersionProvider)
        {
            var apiVersion = apiVersionProvider.GetVersion();

            var apiVersionPath = string.IsNullOrWhiteSpace(apiVersion)
                ? ""
                : $"/v{apiVersion}";
            
            var uriBuilder = new UriBuilder(_authConfig.SiteUrl)
            {
                Scheme = _authConfig.UseHttps ? "https" : "http",
                Path = $"{_authConfig.ServicePathName}{apiVersionPath}/{endpointConfiguration.EndpointAlias}/{path}"
            };

            return uriBuilder.Uri;
        }
    }
}