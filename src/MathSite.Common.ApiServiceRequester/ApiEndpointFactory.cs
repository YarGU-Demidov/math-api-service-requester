using MathSite.Common.ApiServiceRequester.Abstractions;

namespace MathSite.Common.ApiServiceRequester
{
    public class ApiEndpointFactory : IApiEndpointFactory
    {
        private readonly IApiVersionProvider _apiVersionProvider;

        public ApiEndpointFactory(IApiVersionProvider apiVersionProvider)
        {
            _apiVersionProvider = apiVersionProvider;
        }

        public IApiEndpoint GetEndpoint(ServiceMethod serviceMethod)
        {
            return new ApiEndpoint(new ApiEndpointConfiguration(serviceMethod.ServiceName), _apiVersionProvider);
        }
    }
}