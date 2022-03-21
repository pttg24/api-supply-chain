namespace SupplyChainAPI.Configuration
{
    using System.Net.Http;
    using Handlers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System;

    public static class IoC
    {
        public static IServiceConfiguration RegisterServiceConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var serviceConfiguration = new ServiceConfiguration();
            configuration.GetSection(nameof(ServiceConfiguration)).Bind(serviceConfiguration);
            services.AddSingleton((IServiceConfiguration) serviceConfiguration);
            return serviceConfiguration;
        }

        public static void RegisterSupplyChainAPIServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IServiceConfiguration serviceConfiguration)
        {
            services.AddSingleton<IOrdersService, OrdersService>();
            services.AddSingleton<IDeliveriesService, DeliveriesService>();
            services.AddWithHttpClient<IOrdersHandler, OrdersHandler>(serviceConfiguration.OrdersApi);
            services.AddWithHttpClient<IDeliveriesHandler, DeliveriesHandler>(serviceConfiguration.DeliveriesApi);
        }

        private static void AddWithHttpClient<TInt, TImpl>(this IServiceCollection services, IApiConfiguration apiConfiguration) where TImpl : class, TInt where TInt : class
            => services.AddHttpClient<TInt, TImpl>(ConfigureClient(apiConfiguration));

        private static Action<HttpClient> ConfigureClient(IApiConfiguration apiConfiguration) => client =>
        {
            client.BaseAddress = new Uri($"{apiConfiguration.BaseAddress}{apiConfiguration.BasePath}");
            client.DefaultRequestHeaders.Add(
                apiConfiguration.ApiKeyName,
                apiConfiguration.ApiKeyValue);
            client.DefaultRequestHeaders.Add(
                "Accept",
                "application/json");
        };
    }
}