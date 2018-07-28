using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using MathSite.Common.ApiServiceRequester.Versions;
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

            var actual = _uriBuilder.FromPath("path", new ApiEndpointConfiguration("test"), new AnyApiVersionProvider())
                .ToString();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void BuildsCorrectUriWithVersion()
        {
            const string expected = "http://test.localhost:8000/v1/path";
            var apiVersion = new Version(1, 0);

            var actual = _uriBuilder.FromPath("path", new ApiEndpointConfiguration("test"), new SelectedApiVersionProvider(apiVersion))
                .ToString();

            Assert.Equal(expected, actual);
        }
    }
}