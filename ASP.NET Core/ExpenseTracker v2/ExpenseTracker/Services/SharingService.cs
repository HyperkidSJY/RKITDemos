using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Models.ENUM;
using ExpenseTracker.Models.POCO;
using ServiceStack.OrmLite;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Service for handling shared expenses within a group.
    /// </summary>
    public class SharingService : ISharingService
    {
        #region Fields

        private int _id;
        private Response _objResponse;
        private readonly IAppDbConnection _dbConnection;
        private YMS01 _objYS01;
        private double _amountPerUser;
        private List<YMR01> _groupUsers;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SharingService"/> class.
        /// </summary>
        /// <param name="dbConnection">Database connection interface</param>
        public SharingService(IAppDbConnection dbConnection)
        {
            _objResponse = new Response();
            _dbConnection = dbConnection;
            _objYS01 = new YMS01();
            _groupUsers = new List<YMR01>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the sharing type for the expense.
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the sharing data by initializing necessary values like group users and the amount per user.
        /// </summary>
        /// <param name="expenseId">ID of the expense</param>
        /// <param name="groupId">ID of the group</param>
        /// <param name="amount">Total amount to be split</param>
        public void PreSave(long expenseId, int groupId, double amount)
        {
            _id = ((int)expenseId);
            using (var db = _dbConnection.GetDbConnection())
            {
                _groupUsers = db.Select<YMR01>(gu => gu.R01F02 == groupId);
                _amountPerUser = amount / _groupUsers.Count;
            }
        }

        /// <summary>
        /// Saves the expense sharing information to the database.
        /// </summary>
        /// <returns>A response object indicating the result of the save operation</returns>
        public Response Save()
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                foreach (var groupUser in _groupUsers)
                {
                    _objYS01.S01F02 = _id;
                    _objYS01.S01F03 = groupUser.R01F03;
                    _objYS01.S01F04 = _amountPerUser;
                    db.Insert(_objYS01);
                }
            }
            _objResponse.Message = "Expense added and split successfully";
            return _objResponse;
        }

        /// <summary>
        /// Validates the sharing data before saving.
        /// </summary>
        /// <returns>A response object indicating the result of the validation</returns>
        public Response Validate()
        {
            return _objResponse;
        }

        /// <summary>
        /// Marks the expense as paid for the specified sharing record.
        /// </summary>
        /// <param name="sharingId">ID of the sharing record</param>
        /// <returns>A response object indicating the result of the operation</returns>
        public Response MarkExpenseAsPaid(int sharingId)
        {
            using (var db = _dbConnection.GetDbConnection())
            {
                var expenseSharing = db.SingleById<YMS01>(sharingId);
                if (expenseSharing != null)
                {
                    expenseSharing.S01F05 = true;
                    db.Update(expenseSharing);
                    _objResponse.Message = "Expense marked as paid";
                }
                else
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Expense sharing record not found";
                }
            }

            return _objResponse;
        }

        #endregion
    }
}
