using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiRequester
    {
        HttpContext Context { get; }
        Task<T> GetAsync<T>(IApiEndpoint endpoint, string path) where T : class;
    }
}