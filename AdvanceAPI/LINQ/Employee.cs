namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Represents an employee within an organization.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the annual salary of the employee.
        /// </summary>
        public decimal AnnualSalary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the employee is a manager.
        /// </summary>
        public bool IsManager { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the department the employee belongs to.
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
