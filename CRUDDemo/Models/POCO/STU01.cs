namespace CRUDDemo.Models.POCO
{
    /// <summary>
    /// Represents a student entity with basic details like ID, name, department, and semester.
    /// </summary>
    public class STU01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// Gets or sets the department of the student.
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Gets or sets the semester in which the student is enrolled.
        /// </summary>
        public int P01F04 { get; set; }
    }
}
