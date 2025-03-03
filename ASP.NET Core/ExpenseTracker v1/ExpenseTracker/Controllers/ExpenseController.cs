using ExpenseTracker.Filters;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Route("getexpense/{userId}")]
        public Response GetExpenses(int userId)
        {
            _logger.LogInformation("Get Expenses Called");
            return _objExpenseService.GetExpenses(userId);
        }

        [HttpPost]
        [Route("addexpense")]
        public Response AddTask(DTOYME01 newExpense)
        {
            _objExpenseService.Type = EnmType.A;
            _objExpenseService.PreSave(newExpense);

            _objResponse = _objExpenseService.Validate();
            if (!_objResponse.IsError)
            {
                _logger.LogInformation("Expenses added");
                return _objExpenseService.Save();
            }
            _logger.LogInformation("Expenses not added");
            return _objResponse;
        }

        [HttpPut]
        [Route("updateexpense")]
        public Response UpdateTask(DTOYME01 updatedExpense)
        {
            _objExpenseService.Type = EnmType.E;
            _objExpenseService.PreSave(updatedExpense);

            _objResponse = _objExpenseService.Validate();
            if (!_objResponse.IsError)
            {
                _logger.LogInformation("Expenses updated");
                return _objExpenseService.Save();
            }
            _logger.LogInformation("Expenses not updated");
            return _objResponse;
        }

        [HttpDelete]
        [Route("deleteexpense/{id}")]
        public Response DeleteTask(int id)
        {
            _objExpenseService.PreDelete(id);
            _objResponse = _objExpenseService.Validate();
            if (!_objResponse.IsError)
            {
                _logger.LogInformation("Expenses deleted");
                return _objExpenseService.Delete();
            }
            _logger.LogInformation("Expenses not deleted");
            return _objResponse;
        }

        [HttpGet]
        [Route("getexpensesbydaterange/{userId}")]
        public Response GetExpensesByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Get expense by date range");
            return _objExpenseService.GetExpensesByDateRange(userId, startDate, endDate);
        }

        [HttpGet]
        [Route("gettotalexpenses/{userId}")]
        public Response GetTotalExpenses(int userId)
        {
            _logger.LogInformation("total expenses called");
            return _objExpenseService.GetTotalExpenses(userId);
        }

        [HttpGet]
        [Route("getexpensesbysorteddate/{userId}")]
        public Response GetExpensesSortedByDate(int userId)
        {
            _logger.LogInformation("Get expense by sorted date");
            return _objExpenseService.GetExpensesSortedByDate(userId);
        }

        [HttpGet]
        [Route("error")]
        public IActionResult Get()
        {
            throw new InvalidOperationException("Something went wrong");
        }
    }
}
