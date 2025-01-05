// This class provides functionality to generate JWT tokens for user authentication.
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace LibraryManagement.Helpers
{
    /// <summary>
    /// Handles the generation of JWT (JSON Web Token) for user authentication and authorization.
    /// </summary>
    public class JWTManager
    {
        /// <summary>
        /// Generates a JWT token using the provided username and role.
        /// The token includes claims for the username and role, is signed using a symmetric key,
        /// and expires after 60 minutes.
        /// </summary>
        /// <param name="username">The username for the user.</param>
        /// <param name="role">The role assigned to the user.</param>
        /// <returns>A signed JWT token as a string.</returns>
        /// <exception cref="ConfigurationErrorsException">Thrown if the JWT signing key is not configured in the AppSettings.</exception>
        public static string GenerateToken(string username, string role)
        {
            // Retrieve the JWT signing key from the application's configuration settings.
            string jwtKey = ConfigurationSettings.AppSettings["JwtKey"];

            // Check if the JWT key is not found or is empty. If so, throw an exception.
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ConfigurationErrorsException("JWT signing key is not configured in AppSettings.");
            }

            // Create a symmetric security key from the JWT key and define the signing credentials.
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define a list of claims that will be added to the JWT. 
            // In this case, it includes the username and role of the user.
            List<Claim> jwtClaims = new List<Claim>
            {
                new Claim("username", username),  // The username claim
                new Claim(ClaimTypes.Role, role)  // The role claim
            };

            // Create the JWT token with the specified claims, signing credentials, and expiration time (60 minutes).
            JwtSecurityToken token = new JwtSecurityToken(
                claims: jwtClaims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(60)  // Token will expire in 60 minutes
            );

            // Generate and return the JWT token as a string.
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Return the generated token.
            return jwtToken;
        }
    }
}
