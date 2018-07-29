using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public interface IApiRequester
    {
        Task<T> GetAsync<T>(ServiceMethod serviceMethod, Dictionary<string, string> data);
        Task<T> PostAsync<T>(ServiceMethod serviceMethod, Dictionary<string, string> data);
    }
}