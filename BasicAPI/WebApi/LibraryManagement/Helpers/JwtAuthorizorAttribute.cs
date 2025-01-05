using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LibraryManagement.Helpers
{
    /// <summary>
    /// Custom authorization filter attribute to validate JWT tokens and check for required roles.
    /// </summary>
    public class JwtAuthorizorAttribute : AuthorizationFilterAttribute
    {
        private readonly string[] _requiredRole;

        /// <summary>
        /// Constructor to initialize the required roles for authorization.
        /// </summary>
        /// <param name="requiredRole">The roles required to access the endpoint.</param>
        public JwtAuthorizorAttribute(params string[] requiredRole)
        {
            _requiredRole = requiredRole ?? Array.Empty<string>(); // If no roles are provided, set to an empty array
        }

        /// <summary>
        /// Method to validate the JWT token and verify the user's role.
        /// </summary>
        /// <param name="actionContext">The context of the current HTTP action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            // Check if the authorization header is missing or not using the Bearer scheme
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest); // Return 400 Bad Request if authorization header is invalid
                return;
            }

            string token = authHeader.Parameter;

            // Check if token is null or missing
            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest); // Return 400 Bad Request
                return;
            }

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(ConfigurationSettings.AppSettings["JwtKey"]); // Get the JWT key from app settings

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // Validate the token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var identity = principal.Identity as ClaimsIdentity;

                // If the identity is null or not authenticated, return Unauthorized
                if (identity == null || !identity.IsAuthenticated)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    return;
                }

                // Check if the user has the required roles
                if (_requiredRole.Length > 0)
                {
                    var userRoles = identity.FindAll(ClaimTypes.Role).Select(c => c.Value);
                    if (!_requiredRole.Any(role => userRoles.Contains(role))) // If the user doesn't have the required role, return Forbidden
                    {
                        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                        return;
                    }
                }
            }
            catch
            {
                // If an exception occurs, return Unauthorized
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    /// <summary>
    /// Custom action filter attribute to add cache control headers to the response.
    /// </summary>
    public class CacheResultAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; } // Duration in seconds for cache control

        /// <summary>
        /// Adds the cache control headers to the response after the action has executed.
        /// </summary>
        /// <param name="actionExecutedContext">The context of the executed action.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Create a CacheControlHeaderValue to specify caching duration and visibility
            CacheControlHeaderValue cacheControlHeader = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(Duration), // Set the maximum age of the cached response
                Public = true // Indicate that the response can be cached by any cache
            };

            // Set the Cache-Control header in the response
            actionExecutedContext.Response.Headers.CacheControl = cacheControlHeader;

            base.OnActionExecuted(actionExecutedContext); // Continue with the base implementation
        }
    }
}
