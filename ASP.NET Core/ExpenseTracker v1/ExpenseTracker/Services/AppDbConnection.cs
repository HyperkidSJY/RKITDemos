using ExpenseTracker.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseTracker.Services
{
    public class AppDbConnection : IAppDbConnection
    {
        private readonly IConfiguration _configuration;

        public AppDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            OrmLiteConnectionFactory connectionFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            return connectionFactory.Open();
        }
    }
}
