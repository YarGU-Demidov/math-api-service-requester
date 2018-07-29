using MathSite.Common.ApiServiceRequester.Abstractions;

namespace ApiServiceRequesterTests
{
    public class TestApiEndpiontFactory : IApiEndpointFactory
    {
        private readonly IApiEndpoint _apiEndpoint;

        public TestApiEndpiontFactory(IApiEndpoint apiEndpoint)
        {
            _apiEndpoint = apiEndpoint;
        }

        public IApiEndpoint GetEndpoint(ServiceMethod serviceMethod)
        {
            return _apiEndpoint;
        }
    }
}