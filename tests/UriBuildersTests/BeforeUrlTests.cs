using System;
using System.Collections.Generic;
using MathSite.Common.ApiServiceRequester;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UriBuildersTests
{
    public class BeforeUrlTests
    {
        private readonly BeforeDomainServiceUriBuilder _uriBuilder;
        
        public BeforeUrlTests()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>(ConfigurationConstants.SiteUrlKey, "https://localhost:8000"), 
                new KeyValuePair<string, string>(ConfigurationConstants.UseHttpsKey, "false") 
            });
            
            _uriBuilder = new BeforeDomainServiceUriBuilder(configBuilder.Build());
        }

        [Fact]
        public void BuildsCorrectUri()
        {
            const string expected = "http://test.localhost:8000/path";
            
            var actual = _uriBuilder.FromPath("path", new ApiEndpointConfiguration("test"))
                .ToString();
            
            Assert.Equal(expected, actual);
        }
    }
}