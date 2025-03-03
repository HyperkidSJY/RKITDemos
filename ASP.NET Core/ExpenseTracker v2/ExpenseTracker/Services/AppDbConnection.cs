using ExpenseTracker.Interfaces;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Provides database connection functionality for the application.
    /// Implements <see cref="IAppDbConnection"/> to retrieve a database connection.
    /// </summary>
    public class AppDbConnection : IAppDbConnection
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbConnection"/> class.
        /// </summary>
        /// <param name="configuration">Configuration interface for retrieving connection strings.</param>
        public AppDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves a new instance of a database connection.
        /// </summary>
        /// <returns>An open database connection.</returns>
        public IDbConnection GetDbConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            OrmLiteConnectionFactory connectionFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            return connectionFactory.Open();
        }

        #endregion
    }
}
