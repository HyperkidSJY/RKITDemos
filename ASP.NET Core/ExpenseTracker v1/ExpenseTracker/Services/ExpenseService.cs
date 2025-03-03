using ExpenseTracker.Helpers;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using ExpenseTracker.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private int _id;
        private Response _objResponse;
        private readonly IAppDbConnection _dbConnection;
        private YME01 _objYME01;

        public ExpenseService(IAppDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _objResponse = new Response();
            _objYME01 = new YME01();
        }

        public EnmType Type { get; set; }

        public Response GetExpenses(int userId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                _objResponse.Data = db.Select<YME01>(e => e.E01F05 == userId);
            }
            _objResponse.Message = "Expense for User: " + userId;
            return _objResponse;
        }

        public Response GetExpensesByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                _objResponse.Data = db.Select<YME01>(e => e.E01F05 == userId && e.E01F04 >= startDate && e.E01F04 <= endDate);
            }
            _objResponse.Message = $"Expenses for User {userId} between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}";
            return _objResponse;
        }

        public Response GetTotalExpenses(int userId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                var totalExpense = db.SqlScalar<decimal>("SELECT SUM(E01F03) FROM YME01 WHERE E01F05 = @userId", new { userId });
                _objResponse.Data = new { TotalExpense = totalExpense };
            }
            _objResponse.Message = $"Total Expenses for User {userId}";
            return _objResponse;
        }

        public Response GetExpensesSortedByDate(int userId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                _objResponse.Data = db.Select<YME01>(e => e.E01F05 == userId)
                                      .OrderByDescending(e => e.E01F04)
                                      .ToList();
            }
            _objResponse.Message = $"Expenses for User {userId} sorted by date";
            return _objResponse;
        }

        public void PreSave(DTOYME01 objDTO)
        {
            _objYME01 = objDTO.Convert<YME01>();
            if (Type == EnmType.E)
            {
                if (IsExist(objDTO.E01F01))
                {
                    _id = objDTO.E01F01;
                }
            }
        }

        public void PreDelete(int id)
        {
            if (IsExist(id))
            {
                _id = id;
            }
        }

        public Response Validate()
        {
            if (Type == EnmType.E || Type == EnmType.D)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct ID";
                }
            }
            return _objResponse;
        }

        public Response Save()
        {
            if (Type == EnmType.A)
            {
                Add(_objYME01);
            }
            if (Type == EnmType.E)
            {
                Update(_objYME01);
            }
            return _objResponse;
        }

        public void Add(YME01 expense)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Insert(expense);
                }
                _objResponse.Data = new { Expense = expense };
                _objResponse.Message = "Expense Added";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
        }

        public void Update(YME01 expense)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Update(expense);
                }
                _objResponse.Data = new { Expense = expense };
                _objResponse.Message = "Expense Updated";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
        }

        public Response Delete()
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                try
                {
                    db.DeleteById<YME01>(_id);
                    _objResponse.IsError = false;
                    _objResponse.Message = "Expense Deleted";
                }
                catch (Exception ex)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = ex.Message;
                }
                return _objResponse;
            }
        }

        public bool IsExist(int id)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Exists<YME01>(e => e.E01F01.Equals(id));
            }
        }
    }
}
