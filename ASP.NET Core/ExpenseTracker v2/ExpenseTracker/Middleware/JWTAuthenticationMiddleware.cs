using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Middleware
{
    /// <summary>
    /// Middleware for handling JWT authentication.
    /// </summary>
    public class JWTAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private static string jwtKey; // Secret key for token validation

        #region Constructor

        /// <summary>
        /// Initializes the JWTAuthenticationMiddleware with the next middleware in the pipeline.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public JWTAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration;
            jwtKey = _configuration.GetSection("JWTKey")["SecretKey"];
            _next = next;
        }

        #endregion

        #region Middleware Logic

        /// <summary>
        /// Invokes the middleware to validate the JWT token in the request.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the endpoint allows anonymous access
            if (context.GetEndpoint()?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context); // Skip the authentication and continue to the next middleware
                return;
            }

            // Extract the token from the Authorization header
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // Missing token
                await context.Response.WriteAsync("Token is required");
                return;
            }

            try
            {
                // Create the security key from the jwtKey
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                // Set token validation parameters
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey
                };

                // Validate the token and set the HttpContext user
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                context.User = principal;  // Attach the validated principal to HttpContext.User
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Invalid token
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            // Continue with the request pipeline
            await _next(context);
        }

        #endregion
    }
}
