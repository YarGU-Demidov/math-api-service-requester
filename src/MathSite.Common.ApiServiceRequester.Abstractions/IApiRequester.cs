using System.Threading.Tasks;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiRequester
    {
        Task<T> GetAsync<T>(IApiEndpoint endpoint, string path);
    }
}