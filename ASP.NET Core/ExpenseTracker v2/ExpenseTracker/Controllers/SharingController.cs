using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharingController : ControllerBase
    {
        private Response _objResponse;
        private readonly ISharingService _objSharingService;

        public SharingController(ISharingService objSharingService)
        {
            _objSharingService = objSharingService;
            _objResponse = new Response();
        }

        #region Add Expense to Group
        /// <summary>
        /// Adds an expense to a group and saves the sharing information.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="newExpense">The details of the new expense.</param>
        /// <param name="_expenseService">The expense service for saving the expense.</param>
        /// <returns>Response indicating success or failure of the operation.</returns>
        [HttpPost("addexpensetogroup/{groupId}")]
        public IActionResult AddExpenseToGroup([FromServices] IExpenseService _expenseService, [FromBody] DTOYME01 newExpense, int groupId)
        {
            _expenseService.Type = EnmType.A;
            _expenseService.PreSave(newExpense);

            _objResponse = _expenseService.Validate();
            if (!_objResponse.IsError)
            {
                _expenseService.Save();
            }

            _objSharingService.Type = EnmType.A;
            _objSharingService.PreSave(_expenseService.LastId(), groupId, newExpense.E01F03);
            _objSharingService.Validate();

            if (!_objResponse.IsError)
            {
                return Ok(_objSharingService.Save());
            }

            return Ok(_objResponse);
        }
        #endregion

        #region Mark Expense as Paid
        /// <summary>
        /// Marks an expense as paid in the sharing system.
        /// </summary>
        /// <param name="sharingId">The ID of the sharing record.</param>
        /// <returns>Response indicating success or failure of the operation.</returns>
        [HttpPut]
        public IActionResult MarkIsPaid(int sharingId)
        {
            return Ok(_objSharingService.MarkExpenseAsPaid(sharingId));
        }
        #endregion
    }
}
