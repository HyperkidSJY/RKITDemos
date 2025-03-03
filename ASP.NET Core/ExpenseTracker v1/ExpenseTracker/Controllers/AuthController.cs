using ExpenseTracker.Filters;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private Response _objResponse;
        private readonly IUserService _objUserService;

        public AuthController(IUserService objUserService)
        {
            _objResponse = new Response();
            _objUserService = objUserService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public Response Register([FromBody] DTOYMU01 newUser)
        {
            _objUserService.Type = EnmType.A;
            _objUserService.PreSave(newUser);
            _objResponse = _objUserService.Validate();

            if (!_objResponse.IsError)
            {
                _objResponse = _objUserService.Save();
            }

            return _objResponse;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public Response Login(string email, string password)
        {
            bool isExists = _objUserService.IsExists(email, password);
            if (isExists)
            {
                _objResponse.Message = "Token";
                _objResponse.Data = new { Token = _objUserService.GetJWT(email) };
            }
            else
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Credentials are invalid";
            }
            return _objResponse;
        }

        [HttpPut]
        [Route("update")]
        public Response UpdateUser(DTOYMU01 updatedUser)
        {
            _objUserService.Type = EnmType.E;
            _objUserService.PreSave(updatedUser);

            _objResponse = _objUserService.Validate();
            if (!_objResponse.IsError)
            {
                return _objUserService.Save();
            }

            return _objResponse;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public Response DeleteTask(int id)
        {
            _objUserService.PreDelete(id);
            _objResponse = _objUserService.Validate();
            if (!_objResponse.IsError)
            {
                return _objUserService.Delete();
            }

            return _objResponse;
        }
    }
}


