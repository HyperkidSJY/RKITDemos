using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models.POCO
{
    /// <summary>
    /// Represents an expense sharing record between users.
    /// </summary>
    public class YMS01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the expense sharing record.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int S01F01 { get; set; }

        /// <summary>
        /// Gets or sets the expense ID associated with this sharing record.
        /// </summary>
        [ForeignKey(typeof(YME01))]
        public int S01F02 { get; set; }

        /// <summary>
        /// Gets or sets the user ID who is part of the sharing record.
        /// </summary>
        [ForeignKey(typeof(YMU01))]
        public int S01F03 { get; set; }

        /// <summary>
        /// Gets or sets the amount shared by the user.
        /// </summary>
        public double S01F04 { get; set; }

        /// <summary>
        /// Gets or sets the status indicating if the expense has been paid by the user.
        /// </summary>
        public bool S01F05 { get; set; }

        #endregion
    }
}
