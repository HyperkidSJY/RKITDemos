using Newtonsoft.Json;

namespace ExpenseTracker.Models.DTO
{
    /// <summary>
    /// Data transfer object (DTO) for the group record (YMG01).
    /// </summary>
    public class DTOYMG01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the group.
        /// </summary>
        [JsonProperty("G01101")]
        public int G01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        [JsonProperty("G01102")]
        public string G01F02 { get; set; }

        #endregion
    }
}
