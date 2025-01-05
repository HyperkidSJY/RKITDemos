namespace LibraryManagement.Models
{
    /// <summary>
    /// Represents a user of the library system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// This is used for authentication purposes.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// This is used for authentication purposes.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user, such as "Admin" or "User".
        /// Roles are used to determine access permissions in the system.
        /// </summary>
        public string Role { get; set; }
    }
}