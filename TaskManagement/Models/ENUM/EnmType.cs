namespace TaskManagement.Models.ENUM
{
    /// <summary>
    /// Defines the operation types for data handling in the system.
    /// </summary>
    public enum EnmType
    {
        #region Operation Types

        /// <summary>
        /// Represents the "Add" operation type.
        /// Used when a new entity is being added.
        /// </summary>
        A,

        /// <summary>
        /// Represents the "Edit" operation type.
        /// Used when an existing entity is being edited.
        /// </summary>
        E,

        /// <summary>
        /// Represents the "Delete" operation type.
        /// Used when an entity is being deleted.
        /// </summary>
        D

        #endregion
    }
}
