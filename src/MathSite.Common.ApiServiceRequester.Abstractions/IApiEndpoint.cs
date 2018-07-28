using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiEndpoint
    {
        Task<HttpResponseMessage> GetAsync(string path, Cookie authCookie, IServiceUriBuilder serviceUriBuilder);

        Task<HttpResponseMessage> PostAsync(string path, HttpContent data, Cookie authCookie, IServiceUriBuilder serviceUriBuilder);
    }
}