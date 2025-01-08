using CRUDDemo.Models;
using CRUDDemo.Models.DTO;
using CRUDDemo.Models.Enum;

namespace CRUDDemo.BusinessLogic
{
    /// <summary>
    /// Defines the contract for student-related operations.
    /// </summary>
    /// <typeparam name="T">The type of object representing student data (typically a DTO).</typeparam>
    public interface IStudentSerivce<T> where T : class
    {
        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        EnmType type { get; set; }

        /// <summary>
        /// Prepares the student data for saving or updating.
        /// </summary>
        /// <param name="objDTO">The student data transfer object (DTO) to be saved or updated.</param>
        void PreSave(DTOSTU01 objDTO);

        /// <summary>
        /// Prepares for the deletion of a student by verifying if the student exists.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        void PreDelete(int id);

        /// <summary>
        /// Validates the student data before performing any operation.
        /// </summary>
        /// <returns>A Response object indicating validation results.</returns>
        Response Validate();

        /// <summary>
        /// Saves or updates a student based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        Response Save();

        /// <summary>
        /// Deletes a student from the database.
        /// </summary>
        /// <returns>A Response object indicating the success or failure of the delete operation.</returns>
        Response Delete();
    }
}
