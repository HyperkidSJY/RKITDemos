using TaskManagement.Models;
using TaskManagement.Models.ENUM;

namespace TaskManagement.Services
{
    /// <summary>
    /// Defines the contract for data handler services, providing methods for handling data operations such as 
    /// saving, deleting, validating, and performing pre-save or pre-delete actions.
    /// </summary>
    /// <typeparam name="T">The type of the data transfer object (DTO) being handled.</typeparam>
    public interface IDataHandlerServices<T> where T : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        EnmType Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Performs actions before saving the provided object (e.g., validation, conversion, etc.).
        /// </summary>
        /// <param name="objDTO">The data transfer object to be saved.</param>
        void PreSave(T objDTO);

        /// <summary>
        /// Performs actions before deleting the data identified by the provided ID.
        /// </summary>
        /// <param name="id">The ID of the data to be deleted.</param>
        void PreDelete(int id);

        /// <summary>
        /// Validates the data and ensures it meets the necessary criteria before performing any operation (save, delete, etc.).
        /// </summary>
        /// <returns>A response indicating whether validation was successful or failed.</returns>
        Response Validate();

        /// <summary>
        /// Saves the data (either adding or updating it depending on the context).
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        Response Save();

        /// <summary>
        /// Deletes the data identified by the ID.
        /// </summary>
        /// <returns>A response indicating the result of the delete operation.</returns>
        Response Delete();

        #endregion
    }
}
