namespace RequestProcess
{
    /// <summary>
    /// Startup class to configure services and the application's request pipeline.
    /// </summary>
    public class Startup
    {
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
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers to the services container
            services.AddControllers();

            // Add Swagger generation for API documentation
            services.AddSwaggerGen();

            // Register custom middleware to be used
            services.AddTransient<CustomMiddleware>();
        }
        #endregion

        #region Configure
        /// <summary>
        /// Configures the application's request pipeline.
        /// </summary>
        /// <param name="app">The WebApplication instance.</param>
        /// <param name="environment">The web hosting environment.</param>
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Use Swagger and Swagger UI only in development environment
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Map controllers to the application's route
            app.MapControllers();

            #region Middleware Setup
            // Middleware to add custom handling for requests
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("hello\n");
            //    await next(context);
            //});

            // Map custom middleware route
            //app.Map("/middleware", HandleMiddleWare);

            // Simple request handling for the "/hello" path
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("shivam");
            //});

            // Static method to handle a custom middleware
            //static void HandleMiddleWare(IApplicationBuilder app)
            //{
            //    app.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync("hello from custom");
            //    });
            //}

            // Map default controller route (commented out for now)
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            // Enable static file serving
            app.UseStaticFiles();

            #region Custom Middleware
            // Use custom middleware
            //app.UseMiddleware<CustomMiddleware>();
            #endregion

            // Run the application
            app.Run();
        }
        #endregion
    }
}
