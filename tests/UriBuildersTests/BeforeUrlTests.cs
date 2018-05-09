using System.Collections.Generic;
using MathSite.Common.ApiServiceRequester;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace UriBuildersTests
{
    public class BeforeUrlTests
    {
        public BeforeUrlTests()
        {
            var config = new AuthConfig
            {
                SiteUrl = "https://localhost:8000",
                UseHttps = false
            };
            var optionsManager = new OptionsWrapper<AuthConfig>(config);
            _uriBuilder = new BeforeDomainServiceUriBuilder(optionsManager);
        }

        private readonly BeforeDomainServiceUriBuilder _uriBuilder;

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