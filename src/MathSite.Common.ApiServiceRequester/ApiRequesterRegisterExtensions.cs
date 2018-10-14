using System;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.Versions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MathSite.Common.ApiServiceRequester
{
    public static class ApiRequesterRegisterExtensions
    {
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder>(this IServiceCollection services, IConfiguration authConfigurationSource)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
        {
            return AddApiRequester<TDomainServiceUriBuilder, ApiEndpointFactory, AuthDataRetriever>(services, authConfigurationSource);
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder>(this IServiceCollection services, IConfiguration authConfigurationSource, string apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
        {
            return AddApiRequester<TDomainServiceUriBuilder, ApiEndpointFactory, AuthDataRetriever>(services, authConfigurationSource, apiVersion);
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder>(this IServiceCollection services, IConfiguration authConfigurationSource, Version apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
        {
            return AddApiRequester<TDomainServiceUriBuilder, ApiEndpointFactory, AuthDataRetriever>(services, authConfigurationSource, apiVersion);
        }
        


        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TAuthDataRetriever>(this IServiceCollection services, IConfiguration authConfigurationSource)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, ApiEndpointFactory, TAuthDataRetriever>(services, authConfigurationSource)
                .AddScoped<IApiVersionProvider, AnyApiVersionProvider>();
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TAuthDataRetriever>(this IServiceCollection services, IConfiguration authConfigurationSource, string apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, ApiEndpointFactory, TAuthDataRetriever>(services, authConfigurationSource)
                .AddScoped<IApiVersionProvider>(provider => new SelectedApiVersionProvider(apiVersion));
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TAuthDataRetriever>(this IServiceCollection services, IConfiguration authConfigurationSource, Version apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, ApiEndpointFactory, TAuthDataRetriever>(services, authConfigurationSource)
                .AddScoped<IApiVersionProvider>(provider => new SelectedApiVersionProvider(apiVersion));
        }
        
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(this IServiceCollection services, IConfiguration authConfigurationSource)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(services, authConfigurationSource)
                .AddScoped<IApiVersionProvider, AnyApiVersionProvider>();
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(this IServiceCollection services, IConfiguration authConfigurationSource, string apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(services, authConfigurationSource)
                .AddScoped<IApiVersionProvider>(provider => new SelectedApiVersionProvider(apiVersion));
        }
        
        public static IServiceCollection AddApiRequester<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(this IServiceCollection services, IConfiguration authConfigurationSource, Version apiVersion)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            return RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(services, authConfigurationSource)
                .AddScoped<IApiVersionProvider>(provider => new SelectedApiVersionProvider(apiVersion));
        }

        
        
        private static IServiceCollection RegisterDefaultServices<TDomainServiceUriBuilder, TApiEndpointFactory, TAuthDataRetriever>(IServiceCollection services, IConfiguration authConfigurationSource)
            where TDomainServiceUriBuilder : class, IServiceUriBuilder
            where TAuthDataRetriever : class, IAuthDataRetriever
            where TApiEndpointFactory : class, IApiEndpointFactory
        {
            services.AddHttpContextAccessor()
                .AddScoped<IAuthDataRetriever, TAuthDataRetriever>();

            return services
                .AddScoped<IApiRequester, ApiRequester>()
                .AddScoped<IServiceUriBuilder, TDomainServiceUriBuilder>()
                .AddScoped<IApiEndpointFactory, TApiEndpointFactory>()
                .AddOptions()
                .Configure<AuthConfig>(authConfigurationSource);
        }
    }
}