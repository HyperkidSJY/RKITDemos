using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models.POCO
{
    /// <summary>
    /// Represents a group record.
    /// </summary>
    public class YMG01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the group.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int G01F01 { get; set; } // group id

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string G01F02 { get; set; } // group name

        #endregion
    }
}
