using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private Response _objResponse;
        private readonly IGroupService _objGroupService;

        public GroupController(IGroupService objGroupService)
        {
            _objResponse = new Response();
            _objGroupService = objGroupService;
        }

        #region Create Group
        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="newGroup">The details of the new group.</param>
        /// <returns>Response indicating success or failure of the group creation.</returns>
        [HttpPost("creategroup")]
        public IActionResult CreateGroup([FromBody] DTOYMG01 newGroup)
        {
            _objGroupService.Type = EnmType.A;
            _objGroupService.PreSave(newGroup);
            _objResponse = _objGroupService.Validate();

            if (!_objResponse.IsError)
            {
                _objResponse = _objGroupService.Save();
            }

            return Ok(_objResponse);
        }
        #endregion
    }
}
