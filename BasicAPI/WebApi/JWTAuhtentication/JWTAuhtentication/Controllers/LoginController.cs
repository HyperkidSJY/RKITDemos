using JWTAuhtentication.Helpers;
using System.Web.Http;

namespace JWTAuhtentication.Controllers
{
    /// <summary>
    /// Controller for managing user login and JWT token generation.
    /// </summary>
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Authenticates a user and generates a JWT token based on the user's credentials.
        /// </summary>
        /// <param name="user">The user object containing username and password.</param>
        /// <returns>
        /// - <see cref="Ok"/>: Returns the generated JWT token if the credentials are valid.
        /// - <see cref="BadRequest"/>: If the user object is null or malformed.
        /// - <see cref="Unauthorized"/>: If the credentials are invalid.
        /// </returns>
        /// <remarks>
        /// Supports two hardcoded users for demonstration purposes:
        /// - Username: "admin", Password: "password" → Role: Admin
        /// - Username: "user", Password: "password" → Role: User
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public IHttpActionResult Signin([FromBody] Models.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (user.Username == "admin" && user.Password == "password")
            {
                var role = "Admin"; // Admin role
                var token = JWTManager.GenerateToken(user.Username, role);
                return Ok(new { Token = token });
            }
            else if (user.Username == "user" && user.Password == "password")
            {
                var role = "User"; // Regular user role
                var token = JWTManager.GenerateToken(user.Username, role);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
