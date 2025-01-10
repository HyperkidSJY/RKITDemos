using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Web;
using TaskManagement.Models;
using TaskManagement.Models.ENUM;

namespace TaskManagement.Services
{
    /// <summary>
    /// Service class for managing tasks related to users.
    /// </summary>
    public class TaskUserService
    {
        #region Private Fields

        private TSK01 _objTSK01;
        private Response _objResponse;
        private IDbConnectionFactory _dbFactory;
        private int _id;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (Add, Edit, Delete, etc.).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for TaskUserService. Initializes the dependencies.
        /// </summary>
        public TaskUserService()
        {
            _objTSK01 = new TSK01();
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Task Retrieval Methods

        /// <summary>
        /// Retrieves tasks for a specific user by user ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A response containing the list of tasks for the user.</returns>
        public Response GetTasksByUser(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objResponse.Data = db.Select<TSK01>(t => t.K01F06 == id);
            }
            _objResponse.Message = "Tasks for User: " + id;
            return _objResponse;
        }

        /// <summary>
        /// Retrieves tasks for a specific user filtered by completion status.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="isCompleted">The completion status of the tasks (true for completed, false for uncompleted).</param>
        /// <returns>A response containing the list of tasks based on the completion status.</returns>
        public Response GetTasksByCompletionStatus(int userId, bool isCompleted)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    var tasks = db.Select<TSK01>(t => t.K01F06 == userId && t.K01F05 == isCompleted);
                    _objResponse.Data = tasks;
                }
                _objResponse.Message = isCompleted ? "Completed tasks for User: " + userId : "Uncompleted tasks for User: " + userId;
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error fetching tasks: " + ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Searches for tasks by a prefix (in title or description) for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="prefix">The prefix to search for in the task title or description.</param>
        /// <returns>A response containing the list of tasks that match the prefix.</returns>
        public Response SearchTasksByPrefix(int userId, string prefix)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    var tasks = db.Select<TSK01>(t => t.K01F06 == userId &&
                                                      (t.K01F02.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ||
                                                       t.K01F03.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));

                    _objResponse.Data = tasks;
                }
                _objResponse.Message = $"Tasks for User: {userId} with prefix: {prefix}";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error searching tasks: " + ex.Message;
            }
            return _objResponse;
        }

        #endregion

        #region Task Update Methods

        /// <summary>
        /// Marks a task as completed or uncompleted.
        /// </summary>
        /// <param name="taskId">The task ID to update.</param>
        /// <param name="completed">A boolean indicating whether to mark the task as completed (true) or uncompleted (false).</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        public Response MarkTaskCompleted(int taskId, bool completed)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    var task = db.SingleById<TSK01>(taskId);
                    if (task == null)
                    {
                        _objResponse.IsError = true;
                        _objResponse.Message = "Task not found.";
                        return _objResponse;
                    }

                    task.K01F05 = completed;

                    db.Update(task);

                    _objResponse.IsError = false;
                    _objResponse.Message = completed ? "Task marked as completed." : "Task unmarked as completed.";
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = $"Error updating task: {ex.Message}";
            }
            return _objResponse;
        }

        #endregion
    }
}
