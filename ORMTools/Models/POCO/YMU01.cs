using ServiceStack.DataAnnotations;

namespace ORMTools.Models
{
    #region "YMU01 Model"

    /// <summary>
    /// Represents the YMU01 model which is used to map to a student record.
    /// </summary>
    public class YMU01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the department to which the student belongs.
        /// </summary>
        public string U01F03 { get; set; }

        /// <summary>
        /// Gets or sets the semester that the student is currently enrolled in.
        /// </summary>
        public int U01F04 { get; set; }
    }

    #endregion "YMU01 Model"
}
