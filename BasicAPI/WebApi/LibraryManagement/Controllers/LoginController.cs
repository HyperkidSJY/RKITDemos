using LibraryManagement.Helpers;
using LibraryManagement.Models;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication and generating JWT tokens.
    /// </summary>
    [RoutePrefix("api/auth")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Endpoint to sign in a user by validating their username and password, and issuing a JWT token.
        /// </summary>
        /// <param name="user">The user object containing the username and password for authentication.</param>
        /// <returns>An HTTP response with a token if authentication is successful, or an error response if authentication fails.</returns>
        [HttpPost]
        [AllowAnonymous] // Allow access to this action without authentication
        [Route("login")] // Specifies the route for this action
        public IHttpActionResult SignIn([FromBody] User user)
        {
            // Check if the user object is null
            if (user == null)
            {
                return BadRequest(); // Return a 400 Bad Request if user is null
            }

            // Validate the user credentials for the "admin" user
            if (user.Username == "admin" && user.Password == "password")
            {
                var role = "Admin"; // Set the role as Admin for the admin user
                var token = JWTManager.GenerateToken(user.Username, role); // Generate the JWT token for the user
                return Ok(new { Token = token }); // Return a 200 OK response with the generated token
            }
            // Validate the user credentials for the "user" user
            else if (user.Username == "user" && user.Password == "password")
            {
                var role = "User"; // Set the role as User for a regular user
                var token = JWTManager.GenerateToken(user.Username, role); // Generate the JWT token for the user
                return Ok(new { Token = token }); // Return a 200 OK response with the generated token
            }

            // If credentials do not match, return a 401 Unauthorized response
            return Unauthorized();
        }
    }
}
