using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManagement.Helpers
{
    /// <summary>
    /// Helper class to manage JWT (JSON Web Token) generation.
    /// </summary>
    public class JWTManager
    {
        #region Constants

        /// <summary>
        /// The secret key used for signing JWT tokens.
        /// This should be securely stored in production environments.
        /// </summary>
        private const string jwtKey = "thisissecuritykeyofcustomjwttokenaut";

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates a JWT token with the specified email and role.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="role">The role of the user (e.g., Admin, User).</param>
        /// <returns>A JWT token as a string.</returns>
        public static string GenerateToken(string email, string role)
        {
            DateTime createdAt = DateTime.Now; // Token creation time
            DateTime expiresAt = createdAt.AddDays(1); // Token expiration time (1 day from creation)

            // Create security key and signing credentials
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create claims for the token
            ClaimsIdentity jwtClaims = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("email", email), // Email claim
                    new Claim(ClaimTypes.Role, role) // Role claim
                });

            // Create the JWT token
            JwtSecurityTokenHandler jwtSecurity = new JwtSecurityTokenHandler();

            JwtSecurityToken token = jwtSecurity.CreateJwtSecurityToken(
                subject: jwtClaims, // Claims
                issuedAt: createdAt, // Issuance time
                expires: expiresAt, // Expiration time
                signingCredentials: creds); // Signing credentials

            // Return the serialized token
            return jwtSecurity.WriteToken(token);
        }

        #endregion
    }
}
