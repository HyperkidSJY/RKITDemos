using Newtonsoft.Json;

namespace ExpenseTracker.Models.DTO
{
    /// <summary>
    /// Data transfer object (DTO) for the user-group association record (YMR01).
    /// </summary>
    public class DTOYMR01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the user-group association.
        /// </summary>
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the group ID associated with the user.
        /// </summary>
        [JsonProperty("R01102")]
        public int R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the group.
        /// </summary>
        [JsonProperty("R01103")]
        public int R01F03 { get; set; }

        #endregion
    }
}
