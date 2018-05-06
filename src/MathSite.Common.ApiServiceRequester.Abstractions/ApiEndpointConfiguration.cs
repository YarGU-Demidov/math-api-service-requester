namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public class ApiEndpointConfiguration
    {
        public string EndpointAlias { get; set; }

        public ApiEndpointConfiguration(string endpointAlias)
        {
            EndpointAlias = endpointAlias;
        }

        public ApiEndpointConfiguration()
            : this(default)
        {
        }
    }
}