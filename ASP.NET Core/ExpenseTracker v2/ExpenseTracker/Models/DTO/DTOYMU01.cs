using Newtonsoft.Json;

namespace ExpenseTracker.Models.DTO
{
    /// <summary>
    /// Data transfer object (DTO) for the user record (YMU01).
    /// </summary>
    public class DTOYMU01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [JsonProperty("U01101")]
        public int U01F01 { get; set; } // id

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [JsonProperty("U01102")]
        public string U01F02 { get; set; } // email

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [JsonProperty("U01103")]
        public string U01F03 { get; set; } // password

        #endregion
    }
}
