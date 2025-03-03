using ExpenseTracker.Filters;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(CustomExceptionFilter))]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private Response _objResponse;
        private readonly IExpenseService _objExpenseService;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IExpenseService objExpenseService, ILogger<ExpenseController> logger)
        {
            _logger = logger;
            _objResponse = new Response();
            _objExpenseService = objExpenseService;
        }

        #region Get Expense
        /// <summary>
        /// Gets all expenses for a specific user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of expenses.</returns>
        [HttpGet]
        [Route("getexpense/{userId}")]
        public IActionResult GetExpenses(int userId)
        {
            _logger.LogInformation("Get Expenses Called");
            return Ok(_objExpenseService.GetExpenses(userId));
        }
        #endregion

        #region Add Expense
        /// <summary>
        /// Adds a new expense for the user.
        /// </summary>
        /// <param name="newExpense">The expense details to be added.</param>
        /// <returns>Response with status of the operation.</returns>
        [HttpPost]
        [Route("addexpense")]
        public IActionResult AddTask(DTOYME01 newExpense)
        {
            _objExpenseService.Type = EnmType.A;
            _objExpenseService.PreSave(newExpense);

            _objResponse = _objExpenseService.Validate();
            if (!_objResponse.IsError)
            {
                _logger.LogInformation("Expenses added");
                return Ok(_objExpenseService.Save());
            }
            _logger.LogInformation("Expenses not added");
            return Ok(_objResponse);
        }
        #endregion

        #region Update Expense
        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="updatedExpense">The expense details to be updated.</param>
        /// <returns>Response with status of the operation.</returns>
        [HttpPut]
        [Route("updateexpense")]
        public IActionResult UpdateTask(DTOYME01 updatedExpense)
        {
            _objExpenseService.Type = EnmType.E;
            _objExpenseService.PreSave(updatedExpense);

            _objResponse = _objExpenseService.Validate();
            if (!_objResponse.IsError)
            {
                _logger.LogInformation("Expenses updated");
                return Ok(_objExpenseService.Save());
            }
            _logger.LogInformation("Expenses not updated");
            return Ok(_objResponse);
        }
        #endregion

        #region Delete Expense
        /// <summary>
        /// Deletes an expense based on its ID.
        /// </summary>
        /// <param name="id">The expense identifier.</param>
        /// <returns>Response with status of the operation.</returns>
        [HttpDelete]
        [Route("deleteexpense/{id}")]
        public IActionResult DeleteTask(int id)
        {
            _objExpenseService.PreDelete(id);
            _objResponse = _objExpenseService.Validate();
            if (!_objResponse.IsError)
            {
                _logger.LogInformation("Expenses deleted");
                return Ok(_objExpenseService.Delete());
            }
            _logger.LogInformation("Expenses not deleted");
            return  Ok(_objResponse);
        }
        #endregion

        #region Get Expenses By Date Range
        /// <summary>
        /// Gets expenses for a user filtered by a date range.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startDate">Start date for filtering expenses.</param>
        /// <param name="endDate">End date for filtering expenses.</param>
        /// <returns>List of expenses within the date range.</returns>
        [HttpGet]
        [Route("getexpensesbydaterange/{userId}")]
        public IActionResult GetExpensesByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Get expense by date range");
            return Ok(_objExpenseService.GetExpensesByDateRange(userId, startDate, endDate));
        }
        #endregion

        #region Get Total Expenses
        /// <summary>
        /// Gets the total expenses for a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Total expenses of the user.</returns>
        [HttpGet]
        [Route("gettotalexpenses/{userId}")]
        public IActionResult GetTotalExpenses(int userId)
        {
            _logger.LogInformation("total expenses called");
            return Ok(_objExpenseService.GetTotalExpenses(userId));
        }
        #endregion

        #region Get Expenses Sorted By Date
        /// <summary>
        /// Gets all expenses for a user sorted by date.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of expenses sorted by date.</returns>
        [HttpGet]
        [Route("getexpensesbysorteddate/{userId}")]
        public Response GetExpensesSortedByDate(int userId)
        {
            _logger.LogInformation("Get expense by sorted date");
            return _objExpenseService.GetExpensesSortedByDate(userId);
        }
        #endregion

        #region Error Simulation (For Testing)
        /// <summary>
        /// Simulates an error for testing purposes.
        /// </summary>
        /// <returns>Throws an exception.</returns>
        [HttpGet]
        [Route("error")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            _logger.LogDebug("This is a debug message");
            _logger.LogInformation("This is an info message");
            _logger.LogWarning("This is a warning message");
            throw new InvalidOperationException("Something went wrong");
        }

        /// <summary>
        /// changes the log level
        /// </summary>
        [HttpPost("changeLogLevel")]
        public IActionResult ChangeLogLevel([FromQuery] string newLevel)
        {
            var config = LogManager.Configuration;
            if (NLog.LogLevel.FromString(newLevel) is NLog.LogLevel level)
            {
                foreach (var rule in config.LoggingRules)
                {
                    rule.SetLoggingLevels(level, NLog.LogLevel.Fatal);
                }
                LogManager.ReconfigExistingLoggers();
                return Ok($"Log level updated to {newLevel}");
            }
            return BadRequest("Invalid log level.");
        }

        /// <summary>
        /// changes the directory of nLog.
        /// </summary>
        [HttpPut("changDirectory")]
        public IActionResult ChangeLogDirectory([FromBody] string newLogDirectory)
        {
            if (string.IsNullOrEmpty(newLogDirectory))
            {
                return BadRequest("The new log directory is required.");
            }

            // Change the log directory using the LogService
            _objExpenseService.ChangeLogDirectory(newLogDirectory);
            return Ok(new { Message = "Log directory changed successfully." });
        }

        #endregion
    }
}
