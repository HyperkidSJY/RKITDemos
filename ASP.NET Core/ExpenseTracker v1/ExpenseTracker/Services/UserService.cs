using ExpenseTracker.Helpers;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using ExpenseTracker.Models.POCO;
using ServiceStack.OrmLite;

namespace ExpenseTracker.Services
{
    public class UserService : IUserService
    {
        private YMU01 _objYMU01;
        private Response _objResponse;
        private int _id;
        private readonly IAppDbConnection _dbConnection;

        public UserService(IAppDbConnection dbConnection)
        {
            _objYMU01 = new YMU01();
            _objResponse = new Response();
            _dbConnection = dbConnection;
        }

        public EnmType Type { get; set; }

        public void PreSave(DTOYMU01 objDTO)
        {
            _objYMU01 = objDTO.Convert<YMU01>();
            if (Type == EnmType.E)
            {
                if (IsExists(objDTO.U01F01))
                {
                    _id = objDTO.U01F01;
                }
            }
        }

        public void PreDelete(int id)
        {
            if (IsExists(id))
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
                Add(_objYMU01);
            }
            if (Type == EnmType.E)
            {
                Update(_objYMU01);
            }
            return _objResponse;
        }

        public Response Add(YMU01 user)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Insert(user);
                }
                _objResponse.Data = new { Token = GetJWT(user.U01F02) };
                _objResponse.Message = "User Added";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        public void Update(YMU01 user)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Update(user);
                }
                _objResponse.Data = new { User = user };
                _objResponse.Message = "User Updated";
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
                    db.DeleteById<YMU01>(_id);
                    _objResponse.IsError = false;
                    _objResponse.Message = "User Deleted";
                }
                catch (Exception ex)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = ex.Message;
                }
                return _objResponse;
            }
        }


        public YMU01 GetUser(string email, string password)
        {
            using(var db = _dbConnection.GetDbConnection())
            {
                return db.Single<YMU01>(e => e.U01F02.Equals(email) && e.U01F03.Equals(password));
            }
        }

        public bool IsExists(string email, string password)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Exists<YMU01>(e => e.U01F02.Equals(email) && e.U01F03.Equals(password));
            }
        }

        public bool IsExists(int id)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Exists<YMU01>(e => e.U01F01.Equals(id));
            }
        }

        public string GetJWT(string email)
        {
            return JWTManager.GenerateToken(email);
        }
    }
}
