using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Helpers
{
    public class JWTManager
    {
        private const string jwtKey = "thisissecuritykeyofcustomjwttokenaut";

        public static string GenerateToken(string email)
        {
            DateTime createdAt = DateTime.Now;
            DateTime expiresAt = createdAt.AddDays(1);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            ClaimsIdentity jwtClaims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            });

            JwtSecurityTokenHandler jwtSecurity = new JwtSecurityTokenHandler();
            JwtSecurityToken token = jwtSecurity.CreateJwtSecurityToken(
                subject: jwtClaims,
                issuedAt: createdAt,
                expires: expiresAt,
                signingCredentials: creds
            );

            return jwtSecurity.WriteToken(token);
        }
    }
}
