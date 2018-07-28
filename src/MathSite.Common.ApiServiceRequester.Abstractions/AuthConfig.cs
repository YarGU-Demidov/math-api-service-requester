namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public class AuthConfig
    {
        public string SiteUrl { get; set; } = "localhost";
        public bool UseHttps { get; set; } = true;
        public string ServicePathName { get; set; } = "services";
    }
}