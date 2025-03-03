using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceLifetime.BL;
using ServiceLifetime.Extensions;
using ServiceLifetime.Interface;
using System.Net;

namespace ServiceLifetime
{
    /// <summary>
    /// Configures services and middleware for the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the configuration for the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region ConfigureServices
        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controller services
            services.AddControllers();

            // Add Swagger for API documentation
            services.AddSwaggerGen();

            // Add API explorer for Swagger
            services.AddEndpointsApiExplorer();

            // Add application-specific services (commented out options)
            // services.AddSingleton<IProductService, ProductService>();
            // services.AddTransient<IProductService, ProductService>();
            // services.AddScoped<IProductService, ProductService>();
            // services.AddTransient<IProductService, TestService>();
            // services.TryAddTransient<IProductService, TestService>();

            // Add custom item services
            services.AddItemServices();
        }
        #endregion

        #region Configure
        /// <summary>
        /// Configures the middleware pipeline for the application.
        /// </summary>
        /// <param name="app">The web application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure middleware for development environment
            if (environment.IsDevelopment())
            {
                // Enable Swagger UI for API documentation
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure exception handling for production environment
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    await context.Response.WriteAsync(ex.Error.Message);
                                }
                            }
                        );
                    }
                );
            }

            // Map API controllers to routes
            app.MapControllers();

            // Example route for development with a division by zero error
            app.MapGet("/develop", async context =>
            {
                int Number1 = 10, Number2 = 0;
                int Result = Number1 / Number2; // This statement will throw a runtime exception
                await context.Response.WriteAsync($"Result : {Result}");
            });

            // Run the application
            app.Run();
        }
        #endregion
    }
}
