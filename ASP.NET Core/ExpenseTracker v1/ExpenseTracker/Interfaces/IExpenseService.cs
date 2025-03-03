using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.POCO;

namespace ExpenseTracker.Interfaces
{
    public interface IExpenseService : IDataHandler<DTOYME01>
    {
        void Add(YME01 expense);

        Response GetExpenses(int userId);

        void Update(YME01 expense);

        bool IsExist(int id);

        void PreDelete(int id);

        Response Delete();

        Response GetExpensesByDateRange(int userId, DateTime startDate, DateTime endDate);

        Response GetTotalExpenses(int userId);

        Response GetExpensesSortedByDate(int userId);
    }
}
