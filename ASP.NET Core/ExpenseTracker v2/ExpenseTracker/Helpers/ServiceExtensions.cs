using ExpenseTracker.Interfaces;
using ExpenseTracker.Services;

namespace ExpenseTracker.Helpers
{
    /// <summary>
    /// Extension class to register application services with dependency injection.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds the item services (like database connection and business services) to the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to register the services.</param>
        public static void AddItemServices(this IServiceCollection services)
        {
            // Registers the AppDbConnection service for dependency injection.
            services.AddScoped<IAppDbConnection, AppDbConnection>();

            // Registers the UserService for dependency injection.
            services.AddScoped<IUserService, UserService>();

            // Registers the ExpenseService for dependency injection.
            services.AddScoped<IExpenseService, ExpenseService>();

            // Registers the GroupService for dependency injection.
            services.AddScoped<IGroupService, GroupService>();

            // Registers the GroupUserService for dependency injection.
            services.AddScoped<IGroupUserService, GroupUserService>();

            // Registers the SharingService for dependency injection.
            services.AddScoped<ISharingService, SharingService>();

            services.AddSingleton<JWTManager>();
        }
    }
}
