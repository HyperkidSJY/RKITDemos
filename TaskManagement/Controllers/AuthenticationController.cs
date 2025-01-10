using System;
using System.Web.Http;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.POCO;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    /// <summary>
    /// Controller for handling authentication and JWT token generation.
    /// </summary>
    [RoutePrefix("api/auth")]
    [AllowAnonymous]  // This controller is accessible without authentication
    public class AuthenticationController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Service class to handle user-related operations.
        /// </summary>
        private UserService _userService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the UserService instance for user-related operations.
        /// </summary>
        public AuthenticationController()
        {
            _userService = new UserService();
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Generates a JWT token for the user based on provided email and password.
        /// </summary>
        /// <param name="email">User's email for authentication.</param>
        /// <param name="password">User's password for authentication.</param>
        /// <returns>A JWT token if credentials are valid, or an error message if invalid.</returns>
        [HttpGet]
        [Route("generate")]
        public IHttpActionResult GenerateToken(string email, string password)
        {
            Response response = new Response();
            USR01 objUser = _userService.GetUser(email, password);

            // Check if the user exists with the provided credentials
            if (objUser != null)
            {
                response.Data = JWTManager.GenerateToken(email, objUser.R01F06);  // Generate JWT token
            }
            else
            {
                response.IsError = true;
                response.Message = "Credentials are invalid";  // Return error if credentials are invalid
            }

            return Ok(response);  // Return the response
        }

        #endregion
    }
}
