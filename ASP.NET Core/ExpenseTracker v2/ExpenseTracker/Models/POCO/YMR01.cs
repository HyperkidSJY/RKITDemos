using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models.POCO
{
    /// <summary>
    /// Represents a record associating users with groups.
    /// </summary>
    public class YMR01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the record.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the group ID this record belongs to.
        /// </summary>
        [ForeignKey(typeof(YMG01))]
        public int R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the group.
        /// </summary>
        [ForeignKey(typeof(YMU01))]
        public int R01F03 { get; set; }

        #endregion
    }
}
