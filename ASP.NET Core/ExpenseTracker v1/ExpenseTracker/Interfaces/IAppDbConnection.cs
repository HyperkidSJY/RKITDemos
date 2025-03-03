using ServiceStack.Data;
using System.Data;

namespace ExpenseTracker.Interfaces
{
    public interface IAppDbConnection
    {
        IDbConnection GetDbConnection();
    }
}
