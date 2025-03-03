using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Helpers
{
    /// <summary>
    /// Helper class for managing JWT tokens.
    /// </summary>
    public class JWTManager
    {
        private readonly IConfiguration _configuration;
        // Secret key used for signing the JWT token.
        private static string jwtKey;
        public JWTManager(IConfiguration configuration)
        {
            _configuration = configuration;
            jwtKey = _configuration.GetSection("JWTKey")["SecretKey"];

        }


        /// <summary>
        /// Generates a JWT token for a given email.
        /// </summary>
        /// <param name="email">The email of the user for whom the token is being generated.</param>
        /// <returns>A JWT token as a string.</returns>
        public string GenerateToken(string email)
        {
            // The date and time the token was created.
            DateTime createdAt = DateTime.Now;
            // The date and time when the token will expire (1 day from creation).
            DateTime expiresAt = createdAt.AddDays(1);

            // Create a symmetric security key using the secret key.
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            // Define the signing credentials using the HMAC SHA-256 algorithm.
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create a claims identity for the JWT token, which includes the user's email.
            ClaimsIdentity jwtClaims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            });

            // Create a JwtSecurityTokenHandler instance to generate the token.
            JwtSecurityTokenHandler jwtSecurity = new JwtSecurityTokenHandler();
            // Create the JWT token using the claims and credentials.
            JwtSecurityToken token = jwtSecurity.CreateJwtSecurityToken(
                subject: jwtClaims,
                issuedAt: createdAt,
                expires: expiresAt,
                signingCredentials: creds
            );

            // Return the JWT token as a string.
            return jwtSecurity.WriteToken(token);
        }
    }
}
