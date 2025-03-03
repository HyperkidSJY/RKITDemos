using ExpenseTracker.Filters;
using ExpenseTracker.Helpers;
using ExpenseTracker.Middleware;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

namespace ExpenseTracker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                // Add API key definition to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Provide the API key in the header"
                });

                // Add security requirement to require the API key
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                 });
            });
            services.AddEndpointsApiExplorer();
            services.AddItemServices();
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            });
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            });
            services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<JWTAuthenticationMiddleware>();
            app.MapControllers();
            app.Run();
        }

    }
}
