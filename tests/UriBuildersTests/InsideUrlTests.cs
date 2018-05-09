using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
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
                UseHttps = false
            };
            var optionsManager = new OptionsWrapper<AuthConfig>(config);
            _uriBuilder = new AfterDomainServiceUriBuilder(optionsManager);
        }

        private readonly AfterDomainServiceUriBuilder _uriBuilder;

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