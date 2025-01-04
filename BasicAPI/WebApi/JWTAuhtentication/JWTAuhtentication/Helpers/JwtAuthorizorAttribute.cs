using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JWTAuhtentication.Helpers
{
    /// <summary>
    /// Custom authorization filter to validate JWT tokens and enforce role-based access control.
    /// </summary>
    public class JwtAuthorizorAttribute : AuthorizationFilterAttribute
    {
        private readonly string _requiredRole;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthorizorAttribute"/> class.
        /// </summary>
        /// <param name="requiredRole">
        /// The role required to access the action. 
        /// If null, only token validation is performed without role-based checks.
        /// </param>
        public JwtAuthorizorAttribute(string requiredRole = null)
        {
            _requiredRole = requiredRole;
        }

        /// <summary>
        /// Handles JWT token validation and authorization.
        /// </summary>
        /// <param name="actionContext">The HTTP action context for the request.</param>
        /// <remarks>
        /// Checks the authorization header for a valid JWT token. 
        /// If a role is specified, it also verifies that the token contains the required role.
        /// </remarks>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                return;
            }

            var token = authHeader.Parameter;
            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(ConfigurationSettings.AppSettings["JwtKey"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var identity = principal.Identity as ClaimsIdentity;

                if (identity == null || !identity.IsAuthenticated)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    return;
                }

                if (_requiredRole != null && !identity.HasClaim(ClaimTypes.Role, _requiredRole))
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }

    /// <summary>
    /// Custom action filter to enable caching for API responses.
    /// </summary>
    public class CacheResultAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the cache duration in seconds.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Adds cache control headers to the API response.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the executed action.</param>
        /// <remarks>
        /// Sets the "Cache-Control" header to indicate the response can be cached 
        /// for the specified duration.
        /// </remarks>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var cacheControlHeader = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(Duration),
                Public = true
            };

            actionExecutedContext.Response.Headers.CacheControl = cacheControlHeader;

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
