using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private Response _objResponse;
        private readonly IGroupUserService _objGroupUserService;

        public GroupUserController(IGroupUserService objGroupUserService)
        {
            _objGroupUserService = objGroupUserService;
            _objResponse = new Response();
        }

        #region Add User to Group
        /// <summary>
        /// Adds a user to a group.
        /// </summary>
        /// <param name="groupUser">The details of the user and the group they will be added to.</param>
        /// <returns>Response indicating success or failure of the operation.</returns>
        [HttpPost("addusertogroup")]
        public IActionResult CreateGroup([FromBody] DTOYMR01 groupUser)
        {
            _objGroupUserService.Type = EnmType.A;
            _objGroupUserService.PreSave(groupUser);
            _objResponse = _objGroupUserService.Validate();

            if (!_objResponse.IsError)
            {
                _objResponse = _objGroupUserService.Save();
            }

            return Ok(_objResponse);
        }
        #endregion
    }
}
