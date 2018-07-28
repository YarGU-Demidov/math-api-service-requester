using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.Extensions.Options;

namespace MathSite.Common.ApiServiceRequester.UriBuilders
{
    public class BeforeDomainServiceUriBuilder : IServiceUriBuilder
    {
        private readonly AuthConfig _authConfig;

        public BeforeDomainServiceUriBuilder(IOptions<AuthConfig> options)
        {
            _authConfig = options.Value;
        }

        public Uri FromPath(string path, ApiEndpointConfiguration endpointConfiguration, IApiVersionProvider apiVersionProvider)
        {
            var apiVersion = apiVersionProvider.GetVersion();

            var apiVersionPath = string.IsNullOrWhiteSpace(apiVersion)
                ? ""
                : $"/v{apiVersion}";

            path = path[0] == '/' ? path : $"/{path}";
            
            var uriBuilder = new UriBuilder(_authConfig.SiteUrl)
            {
                Scheme = _authConfig.UseHttps ? "https" : "http",
                Path = $"{apiVersionPath}{path}"
            };
            
            uriBuilder.Host = $"{endpointConfiguration.EndpointAlias}.{uriBuilder.Host}";

            return uriBuilder.Uri;
        }
    }
}