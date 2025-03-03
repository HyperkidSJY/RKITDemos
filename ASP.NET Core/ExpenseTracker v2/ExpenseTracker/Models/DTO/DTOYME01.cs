using Newtonsoft.Json;

namespace ExpenseTracker.Models.DTO
{
    /// <summary>
    /// Data transfer object (DTO) for the expense record (YME01).
    /// </summary>
    public class DTOYME01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the expense.
        /// </summary>
        [JsonProperty("E01101")]
        public int E01F01 { get; set; } // id

        /// <summary>
        /// Gets or sets the title or description of the expense.
        /// </summary>
        [JsonProperty("E01102")]
        public string E01F02 { get; set; } // title

        /// <summary>
        /// Gets or sets the amount of the expense.
        /// </summary>
        [JsonProperty("E01103")]
        public double E01F03 { get; set; } // expense

        /// <summary>
        /// Gets or sets the date when the expense occurred.
        /// </summary>
        [JsonProperty("E01104")]
        public DateTime E01F04 { get; set; } // date

        /// <summary>
        /// Gets or sets the user id associated with the expense.
        /// </summary>
        [JsonProperty("E01105")]
        public int E01F05 { get; set; } // user id

        #endregion
    }
}
