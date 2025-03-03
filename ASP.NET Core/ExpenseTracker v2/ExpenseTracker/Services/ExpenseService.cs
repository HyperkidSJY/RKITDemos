using ExpenseTracker.Controllers;
using ExpenseTracker.Helpers;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using ExpenseTracker.Models.POCO;
using NLog;
using NLog.Targets;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Service for managing expense-related operations such as adding, updating, deleting, and fetching expenses.
    /// </summary>
    public class ExpenseService : IExpenseService
    {
        #region Fields

        private int _id;
        private Response _objResponse;
        private readonly IAppDbConnection _dbConnection;
        private YME01 _objYME01;
        private readonly ILogger<ExpenseService> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseService"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection interface</param>
        public ExpenseService(IAppDbConnection dbConnection , ILogger<ExpenseService> logger)
        {
            _logger = logger;
            _dbConnection = dbConnection;
            _objResponse = new Response();
            _objYME01 = new YME01();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves a list of expenses for a specific user.
        /// </summary>
        /// <param name="userId">User ID for which the expenses are fetched</param>
        /// <returns>A response with the list of expenses for the user</returns>
        public Response GetExpenses(int userId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                _objResponse.Data = db.Select<YME01>(e => e.E01F05 == userId);
            }
            _objResponse.Message = "Expense for User: " + userId;
            return _objResponse;
        }

        /// <summary>
        /// Retrieves a list of expenses for a specific user within a date range.
        /// </summary>
        /// <param name="userId">User ID for which the expenses are fetched</param>
        /// <param name="startDate">Start date for the range</param>
        /// <param name="endDate">End date for the range</param>
        /// <returns>A response with the list of expenses for the user within the date range</returns>
        public Response GetExpensesByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                _objResponse.Data = db.Select<YME01>(e => e.E01F05 == userId && e.E01F04 >= startDate && e.E01F04 <= endDate);
            }
            _objResponse.Message = $"Expenses for User {userId} between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}";
            return _objResponse;
        }

        /// <summary>
        /// Retrieves the total expenses for a specific user.
        /// </summary>
        /// <param name="userId">User ID for which the total expenses are calculated</param>
        /// <returns>A response with the total expenses for the user</returns>
        public Response GetTotalExpenses(int userId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                var totalExpense = db.Scalar<decimal>("SELECT SUM(E01F03) FROM YME01 WHERE E01F05 = @userId", new { userId });
                _objResponse.Data = new { TotalExpense = totalExpense };
            }
            _objResponse.Message = $"Total Expenses for User {userId}";
            return _objResponse;
        }

        /// <summary>
        /// Retrieves a list of expenses for a specific user, sorted by date.
        /// </summary>
        /// <param name="userId">User ID for which the expenses are fetched</param>
        /// <returns>A response with the list of expenses for the user sorted by date</returns>
        public Response GetExpensesSortedByDate(int userId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                _objResponse.Data = db.Select<YME01>(e => e.E01F05 == userId)
                                      .OrderByDescending(e => e.E01F04)
                                      .ToList();
            }
            _objResponse.Message = $"Expenses for User {userId} sorted by date";
            return _objResponse;
        }

        /// <summary>
        /// Prepares the expense data for saving by converting the DTO to the entity model.
        /// </summary>
        /// <param name="objDTO">DTO object representing an expense</param>
        public void PreSave(DTOYME01 objDTO)
        {
            _objYME01 = objDTO.Convert<YME01>();
            if (Type == EnmType.E)
            {
                if (IsExist(objDTO.E01F01))
                {
                    _id = objDTO.E01F01;
                }
            }
        }

        /// <summary>
        /// Prepares the expense data for deletion.
        /// </summary>
        /// <param name="id">ID of the expense to be deleted</param>
        public void PreDelete(int id)
        {
            if (IsExist(id))
            {
                _id = id;
            }
        }

        /// <summary>
        /// Validates the expense data before saving or deleting.
        /// </summary>
        /// <returns>A response indicating whether the validation succeeded or failed</returns>
        public Response Validate()
        {
            if (Type == EnmType.E || Type == EnmType.D)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct ID";
                }
            }
            return _objResponse;
        }

        /// <summary>
        /// Saves a new expense or updates an existing expense based on the operation type.
        /// </summary>
        /// <returns>A response indicating the result of the save operation</returns>
        public Response Save()
        {
            if (Type == EnmType.A)
            {
                Add(_objYME01);
            }
            if (Type == EnmType.E)
            {
                Update(_objYME01);
            }
            return _objResponse;
        }

        /// <summary>
        /// Adds a new expense to the database.
        /// </summary>
        /// <param name="expense">The expense object to be added</param>
        public void Add(YME01 expense)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Insert(expense);
                }
                _objResponse.Data = new { Expense = expense };
                _objResponse.Message = "Expense Added";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
        }

        /// <summary>
        /// Updates an existing expense in the database.
        /// </summary>
        /// <param name="expense">The expense object to be updated</param>
        public void Update(YME01 expense)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Update(expense);
                }
                _objResponse.Data = new { Expense = expense };
                _objResponse.Message = "Expense Updated";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
        }

        /// <summary>
        /// Deletes an expense from the database.
        /// </summary>
        /// <returns>A response indicating the result of the delete operation</returns>
        public Response Delete()
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                try
                {
                    db.DeleteById<YME01>(_id);
                    _objResponse.IsError = false;
                    _objResponse.Message = "Expense Deleted";
                }
                catch (Exception ex)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = ex.Message;
                }
                return _objResponse;
            }
        }

        /// <summary>
        /// Checks if an expense with the given ID exists in the database.
        /// </summary>
        /// <param name="id">ID of the expense to check</param>
        /// <returns>True if the expense exists, otherwise false</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Exists<YME01>(e => e.E01F01.Equals(id));
            }
        }

        /// <summary>
        /// Retrieves the last inserted ID from the database.
        /// </summary>
        /// <returns>The last inserted ID</returns>
        public long LastId()
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.LastInsertId();
            }
        }

        public void ChangeLogDirectory(string newLogDirectory)
        {
            try
            {
                // Ensure the new log directory exists.
                if (!Directory.Exists(newLogDirectory))
                {
                    Directory.CreateDirectory(newLogDirectory);
                }

                // Set the new log directory path in the Global Diagnostics Context
                GlobalDiagnosticsContext.Set("LogDirectory", newLogDirectory);
                _logger.LogInformation($"Log directory changed to: {newLogDirectory}");

                // Reload the NLog configuration
                LogManager.ReconfigExistingLoggers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing log directory.");
            }
        }
    }

    #endregion
}
