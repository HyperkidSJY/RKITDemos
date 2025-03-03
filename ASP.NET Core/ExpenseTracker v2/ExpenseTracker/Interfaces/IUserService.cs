using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for handling operations related to user management.
    /// </summary>
    public interface IUserService : IDataHandler<DTOYMU01>
    {
        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user object to be added.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        Response Add(YMU01 user);

        /// <summary>
        /// Updates an existing user in the system.
        /// </summary>
        /// <param name="user">The user object with updated information.</param>
        Response Update(YMU01 user);

        /// <summary>
        /// Retrieves a user by email and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The user object if found, otherwise null.</returns>
        YMU01 GetUser(string email, string password);

        /// <summary>
        /// Checks if a user exists by email and password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        bool IsExists(string email, string password);

        /// <summary>
        /// Checks if a user exists by user ID.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        bool IsExists(int id);

        /// <summary>
        /// Generates a JWT token for the given email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>A JWT token string.</returns>
        string GetJWT(string email);

        /// <summary>
        /// Prepares for deleting a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        void PreDelete(int id);

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <returns>A response indicating the result of the delete operation.</returns>
        Response Delete();
    }
}
