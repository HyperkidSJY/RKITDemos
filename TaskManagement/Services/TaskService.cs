using OfficeOpenXml;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.DTO;
using TaskManagement.Models.ENUM;
using TaskManagement.Models.POCO;

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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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

        public string ConvertTasksToCsv(List<TSK01> tasks)
        {
            var csvBuilder = new StringBuilder();

            // Add the header row
            csvBuilder.AppendLine("TaskId,Title,Description,DueDate,IsCompleted,UserId");

            // Add the task rows
            foreach (var task in tasks)
            {
                csvBuilder.AppendLine($"{task.K01F01},{task.K01F02},{task.K01F03},{task.K01F04},{task.K01F05},{task.K01F06}");
            }

            return csvBuilder.ToString();
        }

        public byte[] GenerateExcelFile(List<TSK01> tasks)
        {
            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Tasks");

                // Add the header row
                worksheet.Cells[1, 1].Value = "TaskId";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "DueDate";
                worksheet.Cells[1, 5].Value = "IsCompleted";
                worksheet.Cells[1, 6].Value = "UserId";

                // Add the task data to the worksheet
                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks[i];
                    worksheet.Cells[i + 2, 1].Value = task.K01F01;  // TaskId
                    worksheet.Cells[i + 2, 2].Value = task.K01F02;  // Title
                    worksheet.Cells[i + 2, 3].Value = task.K01F03;  // Description
                    worksheet.Cells[i + 2, 4].Value = task.K01F04;  // DueDate
                    worksheet.Cells[i + 2, 5].Value = task.K01F05;  // IsCompleted
                    worksheet.Cells[i + 2, 6].Value = task.K01F06;  // UserId
                }

                // Convert the Excel package to a byte array
                return package.GetAsByteArray();
            }
        }

        /// <summary>
        /// Retrieves all tasks along with user details by joining TSK01 (tasks) and USR01 (users).
        /// </summary>
        /// <returns>A response containing tasks with user data.</returns>
        public Response GetTasksWithUserDetails()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                // Define the SQL query to join the TSK01 (tasks) table with the USR01 (users) table
                var q = db.From<TSK01>()
                          .Join<USR01>()  // Implicit join based on foreign key (K01F06 -> R01F01)
                          .Select<TSK01, USR01>((task, user) => new
                          {
                              TaskId = task.K01F01,
                              Title = task.K01F02,
                              Description = task.K01F03,
                              DueDate = task.K01F04,
                              IsCompleted = task.K01F05,
                              UserId = task.K01F06,
                              UserFirstName = user.R01F02,
                              UserLastName = user.R01F03,
                              UserEmail = user.R01F04
                          }); // Select relevant fields from both tables

                // Execute the query and retrieve the results
                var tasksWithUserDetails = db.Select(q);

                // Return the response containing tasks with user data
                _objResponse.Data = tasksWithUserDetails;
                _objResponse.Message = "Tasks with User details retrieved successfully.";
                return _objResponse;
            }
        }

        #endregion
    }
}
