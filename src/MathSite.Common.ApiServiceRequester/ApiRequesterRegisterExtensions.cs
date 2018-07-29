using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.Versions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MathSite.Common.ApiServiceRequester
{
    public static class ApiRequesterRegisterExtensions
    {
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TApiEndpointFactory>(this IServiceCollection services, IConfiguration authConfigconfigurationSource)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory>(services, authConfigconfigurationSource)
                .AddSingleton(new AnyApiVersionProvider());
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TApiEndpointFactory>(this IServiceCollection services, IConfiguration authConfigconfigurationSource, string apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory>(services, authConfigconfigurationSource)
                .AddSingleton(new SelectedApiVersionProvider(apiVersion));
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TApiEndpointFactory>(this IServiceCollection services, IConfiguration authConfigconfigurationSource, Version apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory>(services, authConfigconfigurationSource)
                .AddSingleton(new SelectedApiVersionProvider(apiVersion));
        }

        private static IServiceCollection RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory>(IServiceCollection services, IConfiguration authConfigconfigurationSource)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return services.AddHttpContextAccessor()
                .AddScoped<IApiRequester, ApiRequester>()
                .AddSingleton<IServiceUriBuilder, TDomainServiceUriBuilder>()
                .AddSingleton<ICommonApiEndpoints, CommonApiEndpoints>()
                .AddSingleton<IApiEndpointFactory, TApiEndpointFactory>()
                .AddOptions()
                .Configure<AuthConfig>(authConfigconfigurationSource);
        }
    }
}