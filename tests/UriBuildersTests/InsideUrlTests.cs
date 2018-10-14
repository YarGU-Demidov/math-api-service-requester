using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using MathSite.Common.ApiServiceRequester.Versions;
using Microsoft.Extensions.Options;
using Xunit;

namespace UriBuildersTests
{
    public class InsideUrlTests
    {
        public InsideUrlTests()
        {
            var config = new AuthConfig
            {
                SiteUrl = "https://localhost:8000",
                UseHttps = false,
                ServicePathName = "api"
            };
            var optionsManager = new OptionsWrapper<AuthConfig>(config);
            _uriBuilder = new AfterDomainServiceUriBuilder(optionsManager);
        }

        private readonly AfterDomainServiceUriBuilder _uriBuilder;

        [Fact]
        public void BuildsCorrectUriForService()
        {
            const string expected = "http://localhost:8000/api/test/path";

            var actual = _uriBuilder.FromPath(
                    "path", 
                    new ApiEndpointConfiguration("test"), 
                    new AnyApiVersionProvider()
                ).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildsCorrectUriForServiceWithApiVersion()
        {
            const string expected = "http://localhost:8000/api/v1/test/path";
            var apiVersion = new Version(1, 0);

            var actual = _uriBuilder.FromPath(
                    "path", 
                    new ApiEndpointConfiguration("test"), 
                    new SelectedApiVersionProvider(apiVersion)
                ).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildsCorrectUriWithParams()
        {
            const string expected = "http://localhost:8000/api/v1/test/path?var=asd&var1=qwe";
            var apiVersion = new Version(1, 0);

            var actual = _uriBuilder.FromPath(
                "path?var=asd&var1=qwe", 
                new ApiEndpointConfiguration("test"), 
                new SelectedApiVersionProvider(apiVersion)
            ).ToString();

            Assert.Equal(expected, actual);
        }
    }
}