using ServiceStack.DataAnnotations;

namespace TaskManagement.Models.POCO
{
    /// <summary>
    /// Represents a user entity in the system.
    /// </summary>
    public class USR01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// This is the primary key and auto-incremented.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// This is typically stored in an encrypted format.
        /// </summary>
        public string R01F05 { get; set; }

        /// <summary>
        /// Gets or sets the role of the user (e.g., Admin, User, etc.).
        /// </summary>
        public string R01F06 { get; set; }

        #endregion
    }
}
