using Newtonsoft.Json;
using System;

namespace TaskManagement.Models.DTO
{
    /// <summary>
    /// Represents a data transfer object (DTO) for the user entity.
    /// Used for transferring user data between layers or over a network.
    /// </summary>
    public class DTOUSR01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// This property is mapped to "R01101" in the JSON.
        /// </summary>
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// This property is mapped to "R01102" in the JSON.
        /// </summary>
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// This property is mapped to "R01103" in the JSON.
        /// </summary>
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// This property is mapped to "R01104" in the JSON.
        /// </summary>
        [JsonProperty("R01104")]
        public string R01F04 { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// This property is mapped to "R01105" in the JSON.
        /// </summary>
        [JsonProperty("R01105")]
        public string R01F05 { get; set; }

        /// <summary>
        /// Gets or sets the role of the user (e.g., Admin, User, etc.).
        /// This property is mapped to "R01106" in the JSON.
        /// </summary>
        [JsonProperty("R01106")]
        public string R01F06 { get; set; }

        #endregion
    }
}
