using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ExpenseTracker.Middleware
{
    public class JWTAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string jwtKey = "thisissecuritykeyofcustomjwttokenaut"; // Secret key for token validation

        public JWTAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint()?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context); // Skip the authentication and continue to the next middleware
                return;
            }


            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest; // Missing token
                await context.Response.WriteAsync("Token is required");
                return;
            }

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey
                };

                // Validate the token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                context.User = principal;  // Attach the validated principal to HttpContext.User
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Invalid token
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            await _next(context); // Continue with the request pipeline
        }
    }
}
