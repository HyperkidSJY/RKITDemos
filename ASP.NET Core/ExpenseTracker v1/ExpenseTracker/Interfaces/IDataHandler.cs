using ExpenseTracker.Models;
using ExpenseTracker.Models.ENUM;

namespace ExpenseTracker.Interfaces
{
    public interface IDataHandler<T> where T : class
    {
        EnmType Type { get; set; }

        void PreSave(T objDTO);

        Response Validate();

        Response Save();
    }
}
