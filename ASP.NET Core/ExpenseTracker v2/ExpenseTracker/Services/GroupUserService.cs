using ExpenseTracker.Helpers;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.DTO;
using ExpenseTracker.Models.ENUM;
using ExpenseTracker.Models.POCO;
using ServiceStack.OrmLite;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Service for managing group user operations such as adding users to groups.
    /// </summary>
    public class GroupUserService : IGroupUserService
    {
        #region Fields

        private int _id;
        private Response _objResponse;
        private readonly IAppDbConnection _dbConnection;
        private YMR01 _objYMR01;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupUserService"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection interface</param>
        public GroupUserService(IAppDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _objResponse = new Response();
            _objYMR01 = new YMR01();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the object for saving by converting the DTO to the entity model.
        /// </summary>
        /// <param name="objDTO">DTO object representing a group user</param>
        public void PreSave(DTOYMR01 objDTO)
        {
            _objYMR01 = objDTO.Convert<YMR01>();
        }

        /// <summary>
        /// Validates the data before saving.
        /// </summary>
        /// <returns>A response indicating whether validation succeeded or failed</returns>
        public Response Validate()
        {
            return _objResponse;
        }

        /// <summary>
        /// Saves a new group user based on the operation type.
        /// </summary>
        /// <returns>A response indicating the result of the save operation</returns>
        public Response Save()
        {
            if (Type == EnmType.A)
            {
                Add(_objYMR01);
            }
            return _objResponse;
        }

        /// <summary>
        /// Adds a new group user to the database.
        /// </summary>
        /// <param name="groupUser">The group user object to be added</param>
        public void Add(YMR01 groupUser)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Insert(groupUser);
                }
                _objResponse.Data = new { UserGroup = groupUser };
                _objResponse.Message = "User added successfully";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
        }

        #endregion
    }
}
