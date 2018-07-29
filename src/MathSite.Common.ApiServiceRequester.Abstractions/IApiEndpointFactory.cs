namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiEndpointFactory
    {
        IApiEndpoint GetEndpoint(ServiceMethod serviceMethod);
    }
}