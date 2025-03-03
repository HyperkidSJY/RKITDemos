using ServiceLifetime.BL;
using ServiceLifetime.Interface;

namespace ServiceLifetime.Extensions
{
    /// <summary>
    /// Extension methods for configuring services.
    /// </summary>
    public static class ServiceExtensions
    {
        #region AddItemServices
        /// <summary>
        /// Registers the product service in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to add the service to.</param>
        public static void AddItemServices(this IServiceCollection services)
        {
            // Registering the ProductService as a Singleton for the IProductService interface
            services.AddSingleton<IProductService, ProductService>();
        }
        #endregion
    }
}
