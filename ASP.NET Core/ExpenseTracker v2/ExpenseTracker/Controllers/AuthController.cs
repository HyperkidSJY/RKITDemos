using ExpenseTracker.Filters;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    // Applies the custom exception filter globally to all actions in this controller
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private Response _objResponse;
        private readonly IUserService _objUserService;

        // Constructor that takes in the IUserService to handle user-related operations
        public AuthController(IUserService objUserService)
        {
            _objResponse = new Response(); // Initialize the response object
            _objUserService = objUserService; // Injecting IUserService dependency
        }

        /// <summary>
        /// Handles user registration by creating a new user.
        /// </summary>
        /// <param name="newUser">DTO containing user details to be registered.</param>
        /// <returns>Response containing the result of the registration process.</returns>
        [HttpPost("register")]
        [AllowAnonymous] // Allow anonymous access to the registration endpoint
        public IActionResult Register([FromBody] DTOYMU01 newUser)
        {
            // Set the operation type to 'A' for Add
            _objUserService.Type = EnmType.A;

            // Prepare the user for saving
            _objUserService.PreSave(newUser);

            // Validate before saving
            _objResponse = _objUserService.Validate();

            // If no errors, proceed to save the user
            if (!_objResponse.IsError)
            {
                _objResponse = _objUserService.Save();
            }

            // Return the response with either success or error message
            return Ok(_objResponse);
        }

        /// <summary>
        /// Handles user login by checking credentials and issuing a JWT token.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password.</param>
        /// <returns>Response containing the JWT token if credentials are valid.</returns>
        [HttpGet("login")]
        [AllowAnonymous] // Allow anonymous access to the login endpoint
        public IActionResult Login(string email, string password)
        {
            // Check if the user exists with the provided credentials
            bool isExists = _objUserService.IsExists(email, password);
            if (isExists)
            {
                _objResponse.Message = "Token";
                _objResponse.Data = new { Token = _objUserService.GetJWT(email) }; // Return the JWT token
            }
            else
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Credentials are invalid"; // Invalid credentials
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Handles user update by modifying the user details.
        /// </summary>
        /// <param name="updatedUser">DTO containing updated user details.</param>
        /// <returns>Response with the result of the update operation.</returns>
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(DTOYMU01 updatedUser)
        {
            // Set the operation type to 'E' for Edit
            _objUserService.Type = EnmType.E;

            // Prepare the updated user for saving
            _objUserService.PreSave(updatedUser);

            // Validate before saving
            _objResponse = _objUserService.Validate();
            if (!_objResponse.IsError)
            {
                // If no errors, proceed to save the updated user
                return Ok(_objUserService.Save());
            }

            return Ok(_objResponse); // Return the error response if validation fails
        }

        /// <summary>
        /// Handles user deletion by removing the user from the system.
        /// </summary>
        /// <param name="id">User ID to be deleted.</param>
        /// <returns>Response with the result of the deletion operation.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteTask(int id)
        {
            // Pre-delete operations, validate the user ID
            _objUserService.PreDelete(id);

            // Validate before deleting
            _objResponse = _objUserService.Validate();
            if (!_objResponse.IsError)
            {
                // If no errors, proceed to delete the user
                return Ok(_objUserService.Delete());
            }

            return Ok(_objResponse); // Return the error response if validation fails
        }
    }
}
