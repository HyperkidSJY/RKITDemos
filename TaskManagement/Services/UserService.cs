using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.DTO;
using TaskManagement.Models.ENUM;
using TaskManagement.Models.POCO;

namespace TaskManagement.Services
{
    /// <summary>
    /// Service class for managing user data.
    /// Implements IDataHandlerServices interface for DTOUSR01 type.
    /// </summary>
    public class UserService : IDataHandlerServices<DTOUSR01>
    {

        #region Private Fields

        private USR01 _objUSR01;
        private Response _objResponse;
        private IDbConnectionFactory _dbFactory;
        private int _id;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for UserService. Initializes the dependencies.
        /// </summary>
        public UserService()
        {
            _objUSR01 = new USR01();
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Pre-Operations

        /// <summary>
        /// Pre-delete action to check if the user exists before attempting to delete.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        public void PreDelete(int id)
        {
            if (IsExist(id))
            {
                _id = id;
            }
        }

        /// <summary>
        /// Pre-save action to convert the DTO object and handle specific operations like encryption for add type.
        /// </summary>
        /// <param name="objDTO">The user DTO object to be saved.</param>
        public void PreSave(DTOUSR01 objDTO)
        {
            _objUSR01 = objDTO.Convert<USR01>();
            _objUSR01.R01F05 = Encryption.GetEncryptPassword(_objUSR01.R01F05);
            if (Type == EnmType.E)
            {
                if (IsExist(objDTO.R01F01))
                {
                    _id = objDTO.R01F01;
                }
            }
        }

        #endregion

        #region Save Operations

        /// <summary>
        /// Save the user data depending on the operation type (Add, Edit, etc.).
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save()
        {
            if (Type == EnmType.A)
            {
                return Add(_objUSR01);
            }
            if (Type == EnmType.E)
            {
                return Update(_objUSR01);
            }
            _objResponse.IsError = true;
            _objResponse.Message = "Internal Error";
            return _objResponse;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user object to be added.</param>
        /// <returns>A response indicating the result of the add operation.</returns>
        public Response Add(USR01 user)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(user);
                }
                _objResponse.Message = "User Added";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "User not added";
                throw new Exception(ex.Message);
            }
            return _objResponse;
        }

        #endregion

        public Response Update(USR01 user)
        {

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Update(user);
                }
                _objResponse.Message = "User Updated";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "User not updated";
                throw new Exception(ex.Message);
            }
            return _objResponse;
        }

        #region Validation

        /// <summary>
        /// Validates the user data before performing operations such as edit or delete.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            if (Type == EnmType.E || Type == EnmType.D)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct id";
                }
            }
            return _objResponse;
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Deletes a user from the database by its ID.
        /// </summary>
        /// <returns>A response indicating the result of the delete operation.</returns>
        public Response Delete()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.DeleteById<USR01>(_id);
                    _objResponse.IsError = false;
                    _objResponse.Message = "Data Deleted";
                }
                catch (Exception ex)
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = ex.Message;
                }
                return _objResponse;
            }
        }

        #endregion

        #region User Retrieval

        /// <summary>
        /// Retrieves a user based on the provided email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user object if found, otherwise null.</returns>
        public USR01 GetUser(string email, string password)
        {
            string encryptedPassword = Encryption.GetEncryptPassword(password);
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                var obj = db.Single<USR01>(e => e.R01F04.Equals(email) && e.R01F05.Equals(encryptedPassword));
                return obj;
            }
        }

        #endregion

        #region Existence Check

        /// <summary>
        /// Checks if a user exists based on email and password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        public bool IsExist(string email, string password)
        {
            string encryptedPassword = Encryption.GetEncryptPassword(password);
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(e => e.R01F04.Equals(email) && e.R01F05.Equals(password));
            }
        }

        /// <summary>
        /// Checks if a user exists based on user ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        public bool IsExist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(e => e.R01F01.Equals(id));
            }
        }

        #endregion
    }
}
