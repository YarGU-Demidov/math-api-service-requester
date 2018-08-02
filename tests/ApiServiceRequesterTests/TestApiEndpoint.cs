using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace ApiServiceRequesterTests
{
    public class TestApiEndpoint : IApiEndpoint
    {
        private readonly string _value;

        public TestApiEndpoint(string value)
        {
            _value = value;
        }

        public Cookie GivenCoockie { get; set; }
        public HttpContent GivenData { get; set; }
        public string Path { get; set; }

        public async Task<HttpResponseMessage> GetAsync(string path, Cookie authCookie, IServiceUriBuilder serviceUriBuilder)
        {
            GivenCoockie = authCookie;
            GivenData = null;
            Path = path;
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(_value)};
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent data, Cookie authCookie, IServiceUriBuilder serviceUriBuilder)
        {
            GivenCoockie = authCookie;
            GivenData = data;
            Path = path;
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(_value)};
        }
    }
}