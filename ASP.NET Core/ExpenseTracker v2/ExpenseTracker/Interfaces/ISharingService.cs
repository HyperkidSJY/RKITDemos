using ExpenseTracker.Models;
using ExpenseTracker.Models.ENUM;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for handling operations related to sharing expenses among group members.
    /// </summary>
    public interface ISharingService
    {
        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        EnmType Type { get; set; }

        /// <summary>
        /// Prepares for saving an expense sharing record.
        /// </summary>
        /// <param name="expenseId">The ID of the expense.</param>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="amount">The total amount of the expense.</param>
        void PreSave(long expenseId, int groupId, double amount);

        /// <summary>
        /// Validates the operation before performing any action.
        /// </summary>
        /// <returns>A response indicating whether the validation was successful.</returns>
        Response Validate();

        /// <summary>
        /// Saves the sharing data (i.e., splits the expense among group members).
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        Response Save();

        /// <summary>
        /// Marks the shared expense as paid.
        /// </summary>
        /// <param name="sharingId">The ID of the sharing record to be marked as paid.</param>
        /// <returns>A response indicating whether the expense sharing was successfully marked as paid.</returns>
        Response MarkExpenseAsPaid(int sharingId);
    }
}
