using Newtonsoft.Json;

namespace ORMTools.Models
{
    #region "DTOYMU01 Model"

    /// <summary>
    /// Data Transfer Object (DTO) for the YMU01 model.
    /// Used for transferring student data in API requests and responses.
    /// </summary>
    public class DTOYMU01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        [JsonProperty("U01101")] // id
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        [JsonProperty("U01102")] // name
        public string U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the department to which the student belongs.
        /// </summary>
        [JsonProperty("U01103")] // dept
        public string U01F03 { get; set; }

        /// <summary>
        /// Gets or sets the semester the student is enrolled in.
        /// </summary>
        [JsonProperty("U01104")] // sem
        public int U01F04 { get; set; }
    }

    #endregion "DTOYMU01 Model"
}
