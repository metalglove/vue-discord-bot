using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Vue.Core.Application.Interfaces;
using Vue.Infrastructure.VueService;
using Vue.Infrastructure.VueService.Cache;

namespace Vue.Mapping
{
    /// <summary>
    /// Represents the <see cref="StartupExtensions"/> class.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Adds the default application services to the service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>Returns the service collection.</returns>
        public static IServiceCollection AddDefaultApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient("Vue", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://vuecinemas.nl/");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            serviceCollection.AddHttpClient<IVueService, VueService>("Vue");

            serviceCollection.AddSingleton<VueMovieCacheMonitor>((serviceProvider) => 
            {
                IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                ILogger<VueMovieCacheMonitor> logger = serviceProvider.GetRequiredService<ILogger<VueMovieCacheMonitor>>();
                return new VueMovieCacheMonitor(logger, httpClientFactory.CreateClient("Vue"));
            });
            serviceCollection.AddHostedService((serviceProvider) => serviceProvider.GetService<VueMovieCacheMonitor>());
            serviceCollection.AddSingleton<VueMovieCache>((serviceProvider) =>
            {
                VueMovieCacheMonitor monitor = serviceProvider.GetRequiredService<VueMovieCacheMonitor>();
                return new VueMovieCache(monitor.Movies);
            });
            return serviceCollection;
        }
    }
}