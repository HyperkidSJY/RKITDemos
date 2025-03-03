using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.POCO;

namespace ExpenseTracker.Interfaces
{
    /// <summary>
    /// Interface for handling operations related to groups.
    /// </summary>
    public interface IGroupService : IDataHandler<DTOYMG01>
    {
        /// <summary>
        /// Adds a new group to the system.
        /// </summary>
        /// <param name="group">The group object to be added.</param>
        void Add(YMG01 group);
    }
}
