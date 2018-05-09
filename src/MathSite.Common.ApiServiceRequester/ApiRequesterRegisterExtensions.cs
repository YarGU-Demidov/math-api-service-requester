using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MathSite.Common.ApiServiceRequester
{
    public static class ApiRequesterRegisterExtensions
    {
        public static IServiceCollection AddApiRequester(this IServiceCollection services)
        {
            // TODO: replace to .AddHttpContextAccessor(); in asp.net core 2.1
            // https://github.com/aspnet/Announcements/issues/190
            // https://github.com/aspnet/HttpAbstractions/issues/946
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddSingleton<IServiceUriBuilder, BeforeDomainServiceUriBuilder>();
            
            return services.AddScoped<IApiRequester, ApiRequester>()
                .AddSingleton<ICommonApiEndpoints, CommonApiEndpoints>();
        }
    }
}