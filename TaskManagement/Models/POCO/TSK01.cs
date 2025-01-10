using ServiceStack.DataAnnotations;
using System;
using TaskManagement.Models.POCO;

namespace TaskManagement.Models
{
    /// <summary>
    /// Represents a task entity in the system.
    /// </summary>
    public class TSK01
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// This is the primary key and auto-incremented.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int K01F01 { get; set; }

        /// <summary>
        /// Gets or sets the title of the task.
        /// </summary>
        public string K01F02 { get; set; }

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        public string K01F03 { get; set; }

        /// <summary>
        /// Gets or sets the due date and time for the task.
        /// </summary>
        public DateTime K01F04 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the task is completed.
        /// Default value is false (not completed).
        /// </summary>
        public bool K01F05 { get; set; } = false;

        /// <summary>
        /// Gets or sets the user ID associated with the task.
        /// This field is a foreign key to the USR01 table (user).
        /// </summary>
        [ForeignKey(typeof(USR01))]
        public int K01F06 { get; set; }

        #endregion
    }
}
