using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;

namespace ExpenseTracker.Interfaces
{
    public interface IUserService : IDataHandler<DTOYMU01>
    {
        Response Add(YMU01 user);

        void Update(YMU01 user);
        YMU01 GetUser(string email, string password);

        bool IsExists(string email, string password);

        bool IsExists(int id);
        
        string GetJWT(string email);

        void PreDelete(int id);

        Response Delete();
    }
}