using NLog;
using NLog.Web;

namespace ExpenseTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = LogManager.Setup().LoadConfigurationFromFile("NLog.config").GetCurrentClassLogger();

            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            GlobalDiagnosticsContext.Set("LogDirectory", logPath);

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            Startup startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            WebApplication app = builder.Build();
            startup.Configure(app, builder.Environment);
        }
    }
}