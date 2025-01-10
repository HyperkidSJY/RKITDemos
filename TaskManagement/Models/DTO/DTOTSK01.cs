using Newtonsoft.Json;
using System;

namespace TaskManagement.Models.DTO
{
    /// <summary>
    /// Represents a data transfer object (DTO) for a task entity.
    /// Used for transferring task data between layers or over a network.
    /// </summary>
    public class DTOTSK01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// This property is mapped to "K01101" in the JSON.
        /// </summary>
        [JsonProperty("K01101")]
        public int K01F01 { get; set; }

        /// <summary>
        /// Gets or sets the title of the task.
        /// This property is mapped to "K01102" in the JSON.
        /// </summary>
        [JsonProperty("K01102")]
        public string K01F02 { get; set; }

        /// <summary>
        /// Gets or sets the description of the task.
        /// This property is mapped to "K01103" in the JSON.
        /// </summary>
        [JsonProperty("K01103")]
        public string K01F03 { get; set; }

        /// <summary>
        /// Gets or sets the due date for the task.
        /// This property is mapped to "K01104" in the JSON.
        /// </summary>
        [JsonProperty("K01104")]
        public DateTime K01F04 { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the task.
        /// This property is mapped to "K01105" in the JSON.
        /// </summary>
        [JsonProperty("K01105")]
        public DateTime K01F05 { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the task.
        /// This property is mapped to "K01106" in the JSON.
        /// </summary>
        [JsonProperty("K01106")]
        public int K01F06 { get; set; }

        #endregion
    }
}
