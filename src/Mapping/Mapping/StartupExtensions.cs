using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Vue.Core.Application.Interfaces;
using Vue.Infrastructure.VueService;

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
            serviceCollection.AddHttpClient<IVueService, VueService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://vuecinemas.nl/");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            return serviceCollection;
        }
    }
}