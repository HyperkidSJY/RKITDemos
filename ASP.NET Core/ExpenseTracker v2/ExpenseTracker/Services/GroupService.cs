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
    /// Service for managing group-related operations such as creating groups.
    /// </summary>
    public class GroupService : IGroupService
    {
        #region Fields

        private int _id;
        private Response _objResponse;
        private readonly IAppDbConnection _dbConnection;
        private YMG01 _objYMG01;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupService"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection interface</param>
        public GroupService(IAppDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _objResponse = new Response();
            _objYMG01 = new YMG01();
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
        /// Prepares the group data for saving by converting the DTO to the entity model.
        /// </summary>
        /// <param name="objDTO">DTO object representing a group</param>
        public void PreSave(DTOYMG01 objDTO)
        {
            _objYMG01 = objDTO.Convert<YMG01>();
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
        /// Saves a new group based on the operation type.
        /// </summary>
        /// <returns>A response indicating the result of the save operation</returns>
        public Response Save()
        {
            if (Type == EnmType.A)
            {
                Add(_objYMG01);
            }
            return _objResponse;
        }

        /// <summary>
        /// Adds a new group to the database.
        /// </summary>
        /// <param name="group">The group object to be added</param>
        public void Add(YMG01 group)
        {
            try
            {
                using (var db = _dbConnection.GetDbConnection())
                {
                    db.Insert(group);
                }
                _objResponse.Data = new { Group = group };
                _objResponse.Message = "Group created successfully";
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
