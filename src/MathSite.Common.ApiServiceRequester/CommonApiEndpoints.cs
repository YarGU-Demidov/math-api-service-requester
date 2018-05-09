using MathSite.Common.ApiServiceRequester.Abstractions;

namespace MathSite.Common.ApiServiceRequester
{
    public class CommonApiEndpoints : ICommonApiEndpoints
    {
        public CommonApiEndpoints()
        {
            UsersApi = new ApiEndpoint(new ApiEndpointConfiguration("users"));
            PersonsApi = new ApiEndpoint(new ApiEndpointConfiguration("persons"));
            GroupsApi = new ApiEndpoint(new ApiEndpointConfiguration("groups"));
            ProfessorsApi = new ApiEndpoint(new ApiEndpointConfiguration("professors"));
            AuthApi = new ApiEndpoint(new ApiEndpointConfiguration("auth"));
        }

        public IApiEndpoint UsersApi { get; }
        public IApiEndpoint PersonsApi { get; }
        public IApiEndpoint GroupsApi { get; }
        public IApiEndpoint ProfessorsApi { get; }
        public IApiEndpoint AuthApi { get; }
    }
}