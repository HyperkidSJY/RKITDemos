namespace Routing
{
    #region "Startup Class"
    /// <summary>
    /// The Startup class is used to configure services and the HTTP request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the configuration settings from appsettings.json and other sources.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance containing app settings.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region "ConfigureServices Method"
        /// <summary>
        /// Adds services to the container for dependency injection.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Registering services for Controllers, Swagger, and Endpoint API Explorer
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        #endregion

        #region "Configure Method"
        /// <summary>
        /// Configures the HTTP request pipeline by setting up middlewares and routing.
        /// </summary>
        /// <param name="app">The application builder used to configure the middleware pipeline.</param>
        /// <param name="env">The environment that provides access to environment-specific features (e.g., Development or Production).</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Development environment-specific configurations
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable HTTPS redirection
            app.UseHttpsRedirection();

            // Enable routing and authorization middleware
            app.UseRouting();
            app.UseAuthorization();

            #region "MapWhen Conditions"
            // Mapping for requests that start with /admin in the URL path
            app.MapWhen(
                context => context.Request.Path.StartsWithSegments("/admin"),
                branch => branch.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Admin Area");
                    await next();
                })
            );

            // Mapping for requests with the "special" query parameter
            app.MapWhen(
                context => context.Request.Query.ContainsKey("special"),
                branch => branch.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Special Query parameter detected");
                    await next();
                })
            );

            // Mapping for requests with the "x-custom-header" in the request headers
            app.MapWhen(
                context => context.Request.Headers.ContainsKey("x-custom-header"),
                branch => branch.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Special Header detected");
                    await next();
                })
            );

            // Mapping for requests coming from the "admin.example.com" host
            app.MapWhen(
                context => context.Request.Host.Host == "admin.example.com",
                branch => branch.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Admin Host");
                    await next();
                })
            );

            // Mapping for POST requests to /api
            app.MapWhen(
                context => context.Request.Path.StartsWithSegments("/api") && context.Request.Method == "POST",
                branch => branch.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Post Request to API");
                    await next();
                })
            );
            #endregion

            #region "Endpoint Mapping"
            // Configuring endpoints for the application
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // Map controller routes

                // Basic "Hello, World!" route
                endpoints.MapGet("/", () => "Hello, World!");

                // Route with required student ID as an integer in the URL
                endpoints.MapGet("/students/{id:int}", (int id) =>
                    Results.Ok(new Student { Id = id, Name = $"Student {id}" }));

                // Route with default ID value if not provided
                endpoints.MapGet("/students/default/{id=1}", (int id) =>
                {
                    return $"Student ID: {id}";
                });

                // Route with optional student ID parameter
                endpoints.MapGet("/students/optional/{id?}", (int id) =>
                {
                    return $"Product Id: {id}";
                });

                // POST route for adding products (students in this case)
                endpoints.MapPost("/products", (Student student) =>
                    Results.Created($"/student/{student.Id}", student));

                // PUT route for updating products based on student ID
                endpoints.MapPut("/products/{id:int}", (int id, Student student) =>
                {
                    student.Id = id;
                    return Results.Ok(student);
                });

                // DELETE route for deleting student based on student ID
                endpoints.MapDelete("/students/{id:int}", (int id) =>
                    Results.Ok($"student with ID {id} has been deleted"));
            });
            #endregion
        }
        #endregion
    }
    #endregion
}
