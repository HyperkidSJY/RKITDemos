using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models
{
    /// <summary>
    /// Represents a user in the expense tracker system.
    /// </summary>
    public class YMU01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int U01F01 { get; set; } // id

        /// <summary>
        /// Gets or sets the unique email address for the user.
        /// </summary>
        [Unique]
        public string U01F02 { get; set; } // email

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string U01F03 { get; set; } // password

        #endregion
    }
}
