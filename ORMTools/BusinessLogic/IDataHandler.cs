using ORMTools.Models;
using ORMTools.Models.ENUMS;

namespace ORMTools.BusinessLogic
{
    #region "IDataHandler Interface"

    /// <summary>
    /// Interface for handling data operations, including pre-save, validation, and saving operations for a specific data type.
    /// </summary>
    /// <typeparam name="T">The type of the data being handled, which must be a class.</typeparam>
    public interface IDataHandler<T> where T : class
    {
        #region "Properties"

        /// <summary>
        /// Gets or sets the type of operation (e.g., Add, Edit, Delete).
        /// </summary>
        EnmType Type { get; set; }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Prepares the object for saving by performing any necessary transformations or validations.
        /// </summary>
        /// <param name="objDto">The data transfer object (DTO) to be processed.</param>
        void PreSave(T objDto);

        /// <summary>
        /// Validates the object before saving.
        /// </summary>
        /// <returns>A <see cref="Response"/> indicating the result of the validation.</returns>
        Response Validate();

        /// <summary>
        /// Saves the object to the database or data storage.
        /// </summary>
        /// <returns>A <see cref="Response"/> indicating the result of the save operation.</returns>
        Response Save();

        #endregion "Methods"
    }

    #endregion "IDataHandler Interface"
}
