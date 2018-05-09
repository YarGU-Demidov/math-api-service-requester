using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace ApiServiceRequesterTests
{
    public class TestApiEndpoint : IApiEndpoint
    {
        private readonly string _value;
        
        public Cookie GivenCoockie { get; set; }

        public TestApiEndpoint(string value)
        {
            _value = value;
        }

        public async Task<HttpResponseMessage> GetAsync(string path, Cookie authCookie, IServiceUriBuilder serviceUriBuilder)
        {
            GivenCoockie = authCookie;
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(_value)};
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent data, Cookie authCookie, IServiceUriBuilder serviceUriBuilder)
        {
            GivenCoockie = authCookie;
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(_value)};
        }
    }
}