using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester.UriBuilders
{
    public class AfterDomainServiceUriBuilder : IServiceUriBuilder
    {
        private readonly IConfiguration _configuration;

        public AfterDomainServiceUriBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri FromPath(string path, ApiEndpointConfiguration endpointConfiguration)
        {
            var siteUrl = _configuration[ConfigurationConstants.SiteUrlKey];
            var useHttps = JsonConvert.DeserializeObject<bool>(_configuration[ConfigurationConstants.UseHttpsKey]);

            var uriBuilder = new UriBuilder(siteUrl)
            {
                Scheme = useHttps ? "https" : "http",
                Path = $"services/{endpointConfiguration.EndpointAlias}/{path}"
            };

            return uriBuilder.Uri;
        }
    }
}