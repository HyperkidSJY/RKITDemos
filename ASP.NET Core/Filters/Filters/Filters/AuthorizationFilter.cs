using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    /// <summary>
    /// Custom authorization filter that checks for a valid API key in the request headers.
    /// This filter allows or denies access based on the presence and validity of the API key.
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFilter"/> class.
        /// </summary>
        /// <param name="apiKey">The expected API key value for authorization.</param>
        public AuthorizationFilter(string apiKey)
        {
            _apiKey = apiKey; // Store the expected API key.
        }

        #region OnAuthorization Method
        /// <summary>
        /// This method is invoked during the authorization process to check the request headers for a valid API key.
        /// </summary>
        /// <param name="context">The authorization context containing the HTTP request.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check for API key in request headers
            if (!context.HttpContext.Request.Headers.TryGetValue("My-API-Key", out var apiKeyHeader))
            {
                // If no API key is found in headers, return an Unauthorized response.
                context.Result = new UnauthorizedObjectResult("API key is missing.");
                return;
            }

            // Check if the provided API key matches the expected value
            if (apiKeyHeader != _apiKey)
            {
                // If the API key doesn't match, return an Unauthorized response.
                context.Result = new UnauthorizedObjectResult("Invalid API key.");
                return;
            }

            // If the API key is valid, the request proceeds to the action method.
        }
        #endregion
    }
}
