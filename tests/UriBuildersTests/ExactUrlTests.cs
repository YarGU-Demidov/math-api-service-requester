using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using MathSite.Common.ApiServiceRequester.Versions;
using Xunit;

namespace UriBuildersTests
{
    public class ExactUrlTests
    {
        public ExactUrlTests()
        {
            _uriBuilder = new ExactUrlServiceUrlBuilder();
        }

        private readonly ExactUrlServiceUrlBuilder _uriBuilder;

        [Fact]
        public void TestCustomApiUrl()
        {
            const string expected = "https://localhost:8000/api/path/123";

            var config = new ApiEndpointConfiguration
            {
                EndpointAddress = "https://localhost:8000/api"
            };

            var actual = _uriBuilder.FromPath("path/123", config, new AnyApiVersionProvider()).ToString();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestCustomApiUrlWithConcreteApiVersion()
        {
            const string expected = "https://localhost:8000/api/path/123";

            var config = new ApiEndpointConfiguration
            {
                EndpointAddress = "https://localhost:8000/api"
            };

            var actual = _uriBuilder.FromPath("path/123", config, new SelectedApiVersionProvider("1.0")).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThrowIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => { _uriBuilder.FromPath("/", new ApiEndpointConfiguration(), new AnyApiVersionProvider()); });
        }
    }
}