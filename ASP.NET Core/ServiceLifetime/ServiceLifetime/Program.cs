namespace ServiceLifetime
{
    /// <summary>
    /// The entry point for the application. Initializes and configures the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method which runs the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            #region CreateWebApplicationBuilder
            // Create the WebApplicationBuilder instance using the provided arguments
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            #endregion

            #region ConfigureServices
            // Initialize the Startup class and configure services
            Startup startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            #endregion

            #region BuildAndConfigureApplication
            // Build the web application from the configured builder
            WebApplication app = builder.Build();

            // Configure the application middleware
            startup.Configure(app, builder.Environment);
            #endregion
        }
    }
}
