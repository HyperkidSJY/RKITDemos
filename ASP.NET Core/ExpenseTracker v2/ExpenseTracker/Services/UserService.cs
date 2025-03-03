using ExpenseTracker.Helpers;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using ServiceStack.OrmLite;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Service for managing user-related operations like add, update, delete, validate, etc.
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields

        private YMU01 _objYMU01;
        private Response _objResponse;
        private int _id;
        private readonly IAppDbConnection _dbConnection;
        private JWTManager _jwtManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection interface</param>
        public UserService(IAppDbConnection dbConnection,JWTManager jwtManager)
        {
            _objYMU01 = new YMU01();
            _objResponse = new Response();
            _jwtManager = jwtManager;
            _dbConnection = dbConnection;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user type.
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the object for saving by converting DTO and checking if the ID exists for update.
        /// </summary>
        /// <param name="objDTO">DTO object for user</param>
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

        /// <summary>
        /// Prepares the object for deletion by checking if the ID exists.
        /// </summary>
        /// <param name="id">User ID</param>
        public void PreDelete(int id)
        {
            if (IsExists(id))
            {
                _id = id;
            }
        }

        /// <summary>
        /// Validates the data before saving or deleting the user.
        /// </summary>
        /// <returns>A response object indicating validation result</returns>
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

        /// <summary>
        /// Saves a new user or updates an existing user based on the user type.
        /// </summary>
        /// <returns>A response object indicating save operation result</returns>
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
            _objResponse.IsError = true;
            _objResponse.Message = "Internal Error";
            return _objResponse;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">User object to be added</param>
        /// <returns>A response object indicating the result of the add operation</returns>
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

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="user">User object to be updated</param>
        public Response Update(YMU01 user)
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
            return _objResponse;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <returns>A response object indicating the result of the delete operation</returns>
        public Response Delete()
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                try
                {
                    db.DeleteById<YMU01>(_id);
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

        /// <summary>
        /// Retrieves a user by email and password from the database.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>The user object if found, otherwise null</returns>
        public YMU01 GetUser(string email, string password)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Single<YMU01>(e => e.U01F02.Equals(email) && e.U01F03.Equals(password));
            }
        }

        /// <summary>
        /// Checks if a user exists in the database based on email and password.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>True if the user exists, otherwise false</returns>
        public bool IsExists(string email, string password)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Exists<YMU01>(e => e.U01F02.Equals(email) && e.U01F03.Equals(password));
            }
        }

        /// <summary>
        /// Checks if a user exists in the database based on user ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>True if the user exists, otherwise false</returns>
        public bool IsExists(int id)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                return db.Exists<YMU01>(e => e.U01F01.Equals(id));
            }
        }

        /// <summary>
        /// Generates a JWT token for the user based on email.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>A JWT token string</returns>
        public string GetJWT(string email)
        {
            return _jwtManager.GenerateToken(email);
        }

        #endregion
    }
}
