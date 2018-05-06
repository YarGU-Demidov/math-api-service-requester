namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface ICommonApiEndpoints
    {
        IApiEndpoint UsersApi { get; }
        IApiEndpoint PersonsApi { get; }
        IApiEndpoint GroupsApi { get; }
        IApiEndpoint ProfessorsApi { get; }
        IApiEndpoint AuthApi { get; }
    }
}