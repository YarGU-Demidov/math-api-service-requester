using System.Collections.Generic;
using MathSite.Common.ApiServiceRequester;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UriBuildersTests
{
    public class InsideUrlTests
    {
        private readonly AfterDomainServiceUriBuilder _uriBuilder;
        
        public InsideUrlTests()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>(ConfigurationConstants.SiteUrlKey, "https://localhost:8000"), 
                new KeyValuePair<string, string>(ConfigurationConstants.UseHttpsKey, "false") 
            });
            
            _uriBuilder = new AfterDomainServiceUriBuilder(configBuilder.Build());
        }
        
        [Fact]
        public void BuildsCorrectUriForService()
        {
            const string expected = "http://localhost:8000/services/test/path";
            
            var actual = _uriBuilder.FromPath("path", new ApiEndpointConfiguration("test"))
                .ToString();
            
            Assert.Equal(expected, actual);
        }
    }
}