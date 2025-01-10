using System.Web.Http;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.ENUM;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    /// <summary>
    /// Controller to manage tasks assigned to users. Provides APIs for getting, updating, and searching tasks.
    /// </summary>
    [RoutePrefix("api/user/tasks")]
    [JWTAuthorizor("Manager", "User")]  // Allows "Manager" and "User" roles to access these APIs
    public class TaskUserController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Service class to handle task user operations.
        /// </summary>
        private TaskUserService _objTaskUserService;

        /// <summary>
        /// The response object that contains the result of the API operations.
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the TaskUserService instance.
        /// </summary>
        public TaskUserController()
        {
            _objTaskUserService = new TaskUserService();
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Gets all tasks assigned to a user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch tasks for.</param>
        /// <returns>The tasks assigned to the specified user.</returns>
        [HttpGet]
        [Route("gettasksbyuser/{userId}")]
        public Response GetTasksByUser(int userId)
        {
            return _objTaskUserService.GetTasksByUser(userId);
        }

        /// <summary>
        /// Marks a task as completed or uncompleted based on the provided status.
        /// </summary>
        /// <param name="taskId">The ID of the task to update.</param>
        /// <param name="completed">The completion status (true to mark as completed, false for uncompleted).</param>
        /// <returns>The result of the task update operation.</returns>
        [HttpPut]
        [Route("marktaskcompleted/{taskId}")]
        public Response MarkTaskCompleted(int taskId, [FromUri] bool completed)
        {
            return _objTaskUserService.MarkTaskCompleted(taskId, completed);
        }

        /// <summary>
        /// Gets all completed tasks for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch completed tasks for.</param>
        /// <returns>The completed tasks assigned to the specified user.</returns>
        [HttpGet]
        [Route("completedtasks/{userId}")]
        public Response GetCompletedTasks(int userId)
        {
            return _objTaskUserService.GetTasksByCompletionStatus(userId, true);
        }

        /// <summary>
        /// Gets all uncompleted tasks for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch uncompleted tasks for.</param>
        /// <returns>The uncompleted tasks assigned to the specified user.</returns>
        [HttpGet]
        [Route("uncompletedtasks/{userId}")]
        public Response GetUncompletedTasks(int userId)
        {
            return _objTaskUserService.GetTasksByCompletionStatus(userId, false);
        }

        /// <summary>
        /// Searches for tasks assigned to a user by a prefix string for title or description.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch tasks for.</param>
        /// <param name="prefix">The prefix string to search for in task titles or descriptions.</param>
        /// <returns>The tasks matching the prefix search criteria.</returns>
        [HttpGet]
        [Route("searchtasks/{userId}")]
        public Response SearchTasks(int userId, [FromUri] string prefix)
        {
            return _objTaskUserService.SearchTasksByPrefix(userId, prefix);
        }

        #endregion
    }
}
