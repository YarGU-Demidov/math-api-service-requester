using System.IO;
using System.Threading.Tasks;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiRequester
    {
        Task<T> GetAsync<T>(ServiceMethod serviceMethod, MethodArgs args = null);
        Task<T> PostAsync<T>(ServiceMethod serviceMethod, MethodArgs args = null);

        Task<T> SendDataAsync<T>(ServiceMethod serviceMethod, Stream dataStream);
    }
}