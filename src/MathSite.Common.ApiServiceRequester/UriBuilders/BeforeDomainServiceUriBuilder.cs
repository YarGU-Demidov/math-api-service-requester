using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MathSite.Common.ApiServiceRequester.UriBuilders
{
    public class BeforeDomainServiceUriBuilder : IServiceUriBuilder
    {
        private readonly IConfiguration _configuration;

        public BeforeDomainServiceUriBuilder(IConfiguration configuration)
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
                Path = path
            };
            
            uriBuilder.Host = $"{endpointConfiguration.EndpointAlias}.{uriBuilder.Host}";

            return uriBuilder.Uri;
        }
    }
}