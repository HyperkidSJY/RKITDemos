using System.Data;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for managing database connections.
    /// </summary>
    public interface IAppDbConnection
    {
        /// <summary>
        /// Gets a database connection for interacting with the database.
        /// </summary>
        /// <returns>An <see cref="IDbConnection"/> object that represents the database connection.</returns>
        IDbConnection GetDbConnection();
    }
}
