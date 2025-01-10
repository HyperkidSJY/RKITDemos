using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TaskManagement.Helpers
{
    /// <summary>
    /// Custom authorization filter attribute to validate JWT tokens and user roles.
    /// </summary>
    public class JWTAuthorizorAttribute : AuthorizationFilterAttribute
    {
        #region Private Fields

        /// <summary>
        /// List of roles required for the authorization.
        /// </summary>
        private readonly string[] _requiredRole;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="JWTAuthorizorAttribute"/> class with the specified required roles.
        /// </summary>
        /// <param name="requiredRole">Roles that are required to access the resource.</param>
        public JWTAuthorizorAttribute(params string[] requiredRole)
        {
            _requiredRole = requiredRole ?? Array.Empty<string>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the authorization logic for validating the JWT token and checking the user's role.
        /// </summary>
        /// <param name="actionContext">The context of the HTTP action being executed.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            // Check if the Authorization header is present and uses the Bearer scheme
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest); // Return 400 Bad Request if authorization header is invalid
                return;
            }

            string token = authHeader.Parameter;

            // Check if the token is missing
            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest); // Return 400 Bad Request if token is missing
                return;
            }

            try
            {
                string secretKey = "thisissecuritykeyofcustomjwttokenaut"; // Secret key for signing the token

                // Setup the security key and token handler
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                // Token validation parameters
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey
                };

                // Validate the token
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

                // Check if the identity is valid and authenticated
                if (identity == null || !identity.IsAuthenticated)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized); // Return 401 Unauthorized if the token is invalid
                    return;
                }

                // Check if the user has the required role
                if (_requiredRole.Length > 0)
                {
                    var userRoles = identity.FindAll(ClaimTypes.Role).Select(c => c.Value);
                    if (!_requiredRole.Any(role => userRoles.Contains(role)))
                    {
                        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden); // Return 403 Forbidden if the user lacks the required role
                        return;
                    }
                }
            }
            catch
            {
                // Return 401 Unauthorized if there is an error during token validation
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }

        #endregion
    }
}
