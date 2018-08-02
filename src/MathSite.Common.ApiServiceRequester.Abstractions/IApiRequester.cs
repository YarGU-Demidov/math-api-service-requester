using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiRequester
    {
        Task<T> GetAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, string>> data = null);
        Task<T> PostAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, string>> data = null);
        Task<T> GetAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, IEnumerable<string>>> data);
        Task<T> PostAsync<T>(ServiceMethod serviceMethod, IEnumerable<KeyValuePair<string, IEnumerable<string>>> data);
        Task<T> SendDataAsync<T>(ServiceMethod serviceMethod, Stream dataStream);
    }
}