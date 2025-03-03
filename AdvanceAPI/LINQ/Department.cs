namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Represents a department within an organization.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Gets or sets the unique identifier of the department.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the short name of the department (e.g., "HR").
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the long name of the department (e.g., "Human Resources").
        /// </summary>
        public string LongName { get; set; }
    }
}
