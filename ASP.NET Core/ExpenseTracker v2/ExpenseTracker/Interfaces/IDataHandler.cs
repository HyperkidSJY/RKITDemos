using ExpenseTracker.Models;
using ExpenseTracker.Models.ENUM;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for handling data operations such as save, validation, and preprocessing.
    /// </summary>
    /// <typeparam name="T">The type of the object being handled (e.g., DTOs or entities).</typeparam>
    public interface IDataHandler<T> where T : class
    {
        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete) to be performed.
        /// </summary>
        EnmType Type { get; set; }

        /// <summary>
        /// Prepares the data object for saving by performing necessary transformations or checks.
        /// </summary>
        /// <param name="objDTO">The data transfer object to be preprocessed.</param>
        void PreSave(T objDTO);

        /// <summary>
        /// Validates the data object for correctness and necessary conditions before saving.
        /// </summary>
        /// <returns>A <see cref="Response"/> object indicating the validation result.</returns>
        Response Validate();

        /// <summary>
        /// Saves the data object to the database or storage.
        /// </summary>
        /// <returns>A <see cref="Response"/> object indicating the save result.</returns>
        Response Save();
    }
}
