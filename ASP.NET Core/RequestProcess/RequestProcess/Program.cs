namespace RequestProcess
{
    /// <summary>
    /// Entry point for the application. Configures and runs the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that serves as the entry point for the application.
        /// </summary>
        /// <param name="args">Command line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            #region Create and Configure WebApplicationBuilder
            // Create a builder to configure the application
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            #endregion

            #region Initialize and Configure Startup
            // Initialize the Startup class
            Startup startup = new Startup(builder.Configuration);

            // Call ConfigureServices to register services
            startup.ConfigureServices(builder.Services);
            #endregion

            #region Build and Configure WebApplication
            // Build the WebApplication
            WebApplication app = builder.Build();

            // Call Configure to set up the application's request pipeline
            startup.Configure(app, builder.Environment);
            #endregion
        }
    }
}
