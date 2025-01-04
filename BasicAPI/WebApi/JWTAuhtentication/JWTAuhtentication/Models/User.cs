namespace JWTAuhtentication.Models
{
    /// <summary>
    /// Represents a user in the JWT authentication system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        /// <remarks>
        /// The role can be used to determine access levels or permissions.
        /// </remarks>
        public string Role { get; set; }
    }
}