using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models.POCO
{
    /// <summary>
    /// Represents an expense record.
    /// </summary>
    public class YME01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the expense.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int E01F01 { get; set; } // id

        /// <summary>
        /// Gets or sets the title or description of the expense.
        /// </summary>
        public string E01F02 { get; set; } // title

        /// <summary>
        /// Gets or sets the amount of the expense.
        /// </summary>
        public double E01F03 { get; set; } // expense

        /// <summary>
        /// Gets or sets the date when the expense occurred.
        /// </summary>
        public DateTime E01F04 { get; set; } // date

        /// <summary>
        /// Gets or sets the user id associated with the expense.
        /// </summary>
        [ForeignKey(typeof(YMU01))]
        public int E01F05 { get; set; } // user id

        #endregion
    }
}
