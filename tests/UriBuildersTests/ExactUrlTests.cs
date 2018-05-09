using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using Xunit;

namespace UriBuildersTests
{
    public class ExactUrlTests
    {
        private readonly ExactUrlServiceUrlBuilder _uriBuilder;

        public ExactUrlTests()
        {
            _uriBuilder = new ExactUrlServiceUrlBuilder();
        }
        

        [Fact]
        public void TestCustomApiUrl()
        {
            const string expected = "https://localhost:8000/api/path/123";
            
            var config = new ApiEndpointConfiguration
            {
                EndpointAddress = "https://localhost:8000/api"
            };

            var actual = _uriBuilder.FromPath("path/123", config).ToString();
            
            Assert.Equal(expected, actual);
        }
    }
}