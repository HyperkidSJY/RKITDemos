using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Web;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.DTO;
using TaskManagement.Models.ENUM;

namespace TaskManagement.Services
{
    /// <summary>
    /// Service class for managing tasks.
    /// Implements IDataHandlerServices interface for DTOTSK01 type.
    /// </summary>
    public class TaskService : IDataHandlerServices<DTOTSK01>
    {
        #region Private Fields

        private TSK01 _objTSK01;
        private Response _objResponse;
        private IDbConnectionFactory _dbFactory;
        private int _id;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for TaskService. Initializes the dependencies.
        /// </summary>
        public TaskService()
        {
            _objTSK01 = new TSK01();
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Pre-Operations

        /// <summary>
        /// Pre-delete action to check if the task exists before attempting to delete.
        /// </summary>
        /// <param name="id">The task ID to be deleted.</param>
        public void PreDelete(int id)
        {
            if (IsExist(id))
            {
                _id = id;
            }
        }

        /// <summary>
        /// Pre-save action to convert the DTO object and handle specific operations.
        /// </summary>
        /// <param name="objDTO">The task DTO object to be saved.</param>
        public void PreSave(DTOTSK01 objDTO)
        {
            _objTSK01 = objDTO.Convert<TSK01>();
            _objTSK01.K01F04 = _objTSK01.K01F04.Date; // Ensure the date is in correct format.
            if (Type == EnmType.E)
            {
                if (IsExist(objDTO.K01F01))
                {
                    _id = objDTO.K01F01;
                }
            }
        }

        #endregion

        #region Save and Update Operations

        /// <summary>
        /// Save the task data depending on the operation type (Add or Edit).
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save()
        {
            if (Type == EnmType.A)
            {
                Add(_objTSK01);
            }
            if (Type == EnmType.E)
            {
                Update(_objTSK01);
            }
            return _objResponse;
        }

        /// <summary>
        /// Adds a new task to the database.
        /// </summary>
        /// <param name="task">The task object to be added.</param>
        private void Add(TSK01 task)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(task);
                }
                _objResponse.Message = "Task Added";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Task not added";
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing task in the database.
        /// </summary>
        /// <param name="task">The task object to be updated.</param>
        private void Update(TSK01 task)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Update(task);
                }
                _objResponse.Message = "Task Updated";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Task not updated";
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Deletes a task from the database.
        /// </summary>
        /// <returns>A response indicating the result of the delete operation.</returns>
        public Response Delete()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    db.DeleteById<TSK01>(_id);
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

        #region Validation

        /// <summary>
        /// Validates the task data before performing operations such as edit or delete.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
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

        #endregion

        #region Task Retrieval Methods

        /// <summary>
        /// Retrieves all tasks from the database.
        /// </summary>
        /// <returns>A response containing all tasks.</returns>
        public Response GetAllTasks()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Select<TSK01>();
            }
            _objResponse.Message = "All tasks retrieved successfully";
            return _objResponse;
        }

        /// <summary>
        /// Retrieves tasks for a specific user by user ID.
        /// </summary>
        /// <param name="id">The user ID to filter tasks.</param>
        /// <returns>A response containing the list of tasks for the specified user.</returns>
        public Response GetTasksByUser(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Select<TSK01>(t => t.K01F06 == id);
            }
            _objResponse.Message = "Tasks for User: " + id;
            return _objResponse;
        }

        #endregion

        #region Existence Check

        /// <summary>
        /// Checks if a task exists by ID.
        /// </summary>
        /// <param name="id">The task ID to check.</param>
        /// <returns>True if the task exists, otherwise false.</returns>
        private bool IsExist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<TSK01>(e => e.K01F01.Equals(id));
            }
        }

        #endregion
    }
}
