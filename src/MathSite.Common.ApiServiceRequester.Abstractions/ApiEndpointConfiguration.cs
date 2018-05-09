namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public class ApiEndpointConfiguration
    {
        public ApiEndpointConfiguration(string endpointAlias)
        {
            EndpointAlias = endpointAlias;
        }

        public ApiEndpointConfiguration()
            : this(default)
        {
        }

        public string EndpointAlias { get; set; }
        public string EndpointAddress { get; set; }
    }
}