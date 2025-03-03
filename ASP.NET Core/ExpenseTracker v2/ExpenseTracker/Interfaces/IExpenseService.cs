using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.POCO;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for handling operations related to expenses.
    /// </summary>
    public interface IExpenseService : IDataHandler<DTOYME01>
    {
        /// <summary>
        /// Adds a new expense to the system.
        /// </summary>
        /// <param name="expense">The expense object to be added.</param>
        void Add(YME01 expense);

        /// <summary>
        /// Retrieves all expenses for a specific user.
        /// </summary>
        /// <param name="userId">The user ID whose expenses need to be fetched.</param>
        /// <returns>A <see cref="Response"/> object containing the user's expenses.</returns>
        Response GetExpenses(int userId);

        /// <summary>
        /// Updates an existing expense in the system.
        /// </summary>
        /// <param name="expense">The expense object with updated information.</param>
        void Update(YME01 expense);

        /// <summary>
        /// Checks whether an expense with the given ID exists.
        /// </summary>
        /// <param name="id">The expense ID to check for existence.</param>
        /// <returns><c>true</c> if the expense exists, otherwise <c>false</c>.</returns>
        bool IsExist(int id);

        /// <summary>
        /// Prepares the system for deleting an expense by setting the expense ID.
        /// </summary>
        /// <param name="id">The expense ID to be deleted.</param>
        void PreDelete(int id);

        /// <summary>
        /// Deletes an expense from the system.
        /// </summary>
        /// <returns>A <see cref="Response"/> object indicating the result of the delete operation.</returns>
        Response Delete();

        /// <summary>
        /// Retrieves all expenses for a specific user within a date range.
        /// </summary>
        /// <param name="userId">The user ID whose expenses need to be fetched.</param>
        /// <param name="startDate">The start date for filtering expenses.</param>
        /// <param name="endDate">The end date for filtering expenses.</param>
        /// <returns>A <see cref="Response"/> object containing the user's expenses within the date range.</returns>
        Response GetExpensesByDateRange(int userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Retrieves the total expenses for a specific user.
        /// </summary>
        /// <param name="userId">The user ID whose total expenses are to be fetched.</param>
        /// <returns>A <see cref="Response"/> object containing the total expenses for the user.</returns>
        Response GetTotalExpenses(int userId);

        /// <summary>
        /// Retrieves all expenses for a specific user, sorted by date.
        /// </summary>
        /// <param name="userId">The user ID whose expenses need to be fetched.</param>
        /// <returns>A <see cref="Response"/> object containing the user's expenses sorted by date.</returns>
        Response GetExpensesSortedByDate(int userId);

        /// <summary>
        /// Retrieves the last inserted expense ID.
        /// </summary>
        /// <returns>The ID of the last inserted expense.</returns>
        long LastId();

        void ChangeLogDirectory(string newPath);
    }
}
