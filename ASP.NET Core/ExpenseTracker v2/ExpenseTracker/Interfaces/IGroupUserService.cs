using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.POCO;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for handling operations related to group users.
    /// </summary>
    public interface IGroupUserService : IDataHandler<DTOYMR01>
    {
        /// <summary>
        /// Adds a new user to a group.
        /// </summary>
        /// <param name="groupUser">The group user object to be added.</param>
        void Add(YMR01 groupUser);
    }
}
