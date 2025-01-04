using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuhtentication.Helpers
{
    /// <summary>
    /// Provides functionality for managing JSON Web Tokens (JWT).
    /// </summary>
    public class JWTManager
    {
        /// <summary>
        /// Generates a JWT token for a given username and role.
        /// </summary>
        /// <param name="username">The username of the user for whom the token is being generated.</param>
        /// <param name="role">The role of the user to be included in the token claims.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        /// <remarks>
        /// The token is signed using the key specified in the application's configuration 
        /// and expires after 30 minutes.
        /// </remarks>
        /// <exception cref="ConfigurationErrorsException">
        /// Thrown if the "JwtKey" is missing from the application configuration.
        /// </exception>
        public static string GenerateToken(string username, string role)
        {
            // Retrieve the JWT signing key from the application configuration
            var jwtKey = ConfigurationSettings.AppSettings["JwtKey"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ConfigurationErrorsException("JWT signing key is not configured in AppSettings.");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the claims for the token
            List<Claim> jwtClaims = new List<Claim>
            {
                new Claim("username", username),
                new Claim(ClaimTypes.Role, role)
            };

            // Create the token with claims, expiry, and signing credentials
            JwtSecurityToken token = new JwtSecurityToken(
                claims: jwtClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            // Return the serialized token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
