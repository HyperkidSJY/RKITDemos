using ExpenseTracker.Interfaces;
using ExpenseTracker.Middleware;
using ExpenseTracker.Services;

namespace ExpenseTracker.Helpers
{
    public static class ServiceExtensions
    {
        public static void AddItemServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDbConnection, AppDbConnection>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpenseService, ExpenseService>();
        }
    }
}
