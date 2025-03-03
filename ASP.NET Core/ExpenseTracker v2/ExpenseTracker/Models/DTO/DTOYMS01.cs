using ExpenseTracker.Models.POCO;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace ExpenseTracker.Models.DTO
{
    /// <summary>
    /// Data transfer object (DTO) for the expense sharing record (YMS01).
    /// </summary>
    public class DTOYMS01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the expense sharing record.
        /// </summary>
        [JsonProperty("S01101")]
        public int S01F01 { get; set; }

        /// <summary>
        /// Gets or sets the expense ID associated with the sharing record.
        /// </summary>
        [JsonProperty("S01102")]
        public int S01F02 { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the expense sharing record.
        /// </summary>
        [JsonProperty("S01103")]
        public int S01F03 { get; set; }

        /// <summary>
        /// Gets or sets the amount shared for the expense.
        /// </summary>
        [JsonProperty("S01104")]
        public double S01F04 { get; set; }

        /// <summary>
        /// Gets or sets whether the expense has been marked as paid.
        /// </summary>
        [JsonProperty("S01105")]
        public bool S01F05 { get; set; }

        #endregion
    }
}
