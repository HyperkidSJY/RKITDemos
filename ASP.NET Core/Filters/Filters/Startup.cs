using Filters.Filters;
using Microsoft.OpenApi.Models;

namespace Filters
{
    public class Startup
    {
        /// <summary>
        /// Gets the configuration for the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region ConfigureServices

        /// <summary>
        /// Configures the services required by the application.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add support for controllers
            services.AddControllers();

            // Add services for Swagger/OpenAPI documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                #region Swagger Security Setup
                // Add API key definition to Swagger
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header, // API key should be in the header
                    Name = "My-API-Key", // The name of the API key header
                    Type = SecuritySchemeType.ApiKey, // Defining it as an API key type
                    Description = "Provide the API key in the header" // Description for the API key
                });

                // Add security requirement to require the API key in every request
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] {}
                    }
                });
                #endregion

            });

            #region Register Custom Filters
            // Registering filters to be injected into controllers
            services.AddScoped<AuthorizationFilter>(provider =>
                new AuthorizationFilter("shivam"));
            services.AddScoped<ActionFilter>();
            services.AddScoped<ResultFilter>();
            services.AddScoped<ExceptionFilter>();
            services.AddScoped<ResourceFilter>();
            #endregion
        }
        #endregion

        #region Configure

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The web application to configure.</param>
        /// <param name="env">The environment the application is running in (Development, Production, etc.).</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            #region Development Mode Setup
            if (env.IsDevelopment())
            {
                // Enable Swagger in Development mode
                app.UseSwagger();
                app.UseSwaggerUI(); // Swagger UI allows us to interact with the API documentation
            }
            #endregion

            #region Middleware Configuration
            // Adding middleware to use authorization (API key requirement)
            app.UseAuthorization();

            // Mapping controllers to routes
            app.MapControllers();
            #endregion

            #region Run Application
            // Running the application
            app.Run();
            #endregion
        }

        #endregion
    }
}
