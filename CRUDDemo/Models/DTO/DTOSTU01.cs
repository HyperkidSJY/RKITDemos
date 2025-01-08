using Newtonsoft.Json;

namespace CRUDDemo.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for student, used for mapping data between the front-end and back-end.
    /// </summary>
    public class DTOSTU01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        [JsonProperty("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Gets or sets the department of the student.
        /// </summary>
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Gets or sets the semester in which the student is enrolled.
        /// </summary>
        [JsonProperty("P01104")]
        public int P01F04 { get; set; }
    }
}
