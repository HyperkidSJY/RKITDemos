using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.DTO;
using TaskManagement.Models.ENUM;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    /// <summary>
    /// Controller to manage tasks for admin users. Provides APIs for adding, updating, deleting, and fetching tasks.
    /// </summary>
    [RoutePrefix("api/admin/tasks")]
    [JWTAuthorizor("Manager")]  // Only accessible by users with the "Manager" role
    public class TaskController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Service class to handle task operations.
        /// </summary>
        private TaskService _objTaskService;

        /// <summary>
        /// The response object that holds the result of API operations.
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the TaskService instance for task operations.
        /// </summary>
        public TaskController()
        {
            _objTaskService = new TaskService();
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Adds a new task to the system.
        /// </summary>
        /// <param name="newTask">The task object containing task details.</param>
        /// <returns>The result of the save operation.</returns>
        [HttpPost]
        [Route("addtask")]
        public Response AddTask(DTOTSK01 newTask)
        {
            _objTaskService.Type = EnmType.A;
            _objTaskService.PreSave(newTask);

            _objResponse = _objTaskService.Validate();
            if (!_objResponse.IsError)
            {
                return _objTaskService.Save();
            }
            return _objResponse;
        }

        /// <summary>
        /// Updates an existing task in the system.
        /// </summary>
        /// <param name="updatedTask">The task object with updated details.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut]
        [Route("updatetask")]
        public Response UpdateTask(DTOTSK01 updatedTask)
        {
            _objTaskService.Type = EnmType.E;
            _objTaskService.PreSave(updatedTask);

            _objResponse = _objTaskService.Validate();
            if (_objResponse.IsError)
            {
                return _objResponse;
            }

            return _objTaskService.Save();
        }

        /// <summary>
        /// Deletes an existing task based on the provided task ID.
        /// </summary>
        /// <param name="id">The ID of the task to be deleted.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete]
        [Route("deletetask/{id}")]
        public Response DeleteTask(int id)
        {
            _objTaskService.PreDelete(id);
            _objResponse = _objTaskService.Validate();
            if (!_objResponse.IsError)
            {
                return _objTaskService.Delete();
            }

            return _objResponse;
        }

        /// <summary>
        /// Fetches all tasks from the system.
        /// </summary>
        /// <returns>All tasks in the system.</returns>
        [HttpGet]
        [Route("getalltasks")]
        public Response GetAllTasks()
        {
            return _objTaskService.GetAllTasks();
        }

        /// <summary>
        /// Fetches tasks assigned to a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch tasks for.</param>
        /// <returns>The tasks assigned to the specified user.</returns>
        [HttpGet]
        [Route("gettasksbyuser/{userId}")]
        public Response GetTasksByUser(int userId)
        {
            return _objTaskService.GetTasksByUser(userId);
        }

        [HttpGet]
        [Route("exporttasks")]
        public HttpResponseMessage ExportTasksToCsv()
        {
            var tasksResponse = _objTaskService.GetAllTasks();
            var tasks = tasksResponse.Data as List<TSK01>;
            var csvData = _objTaskService.ConvertTasksToCsv(tasks);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(csvData, Encoding.UTF8, "text/csv");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "tasks.csv"
            };

            //var excelFile = _objTaskService.GenerateExcelFile(tasks);
            //var response = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new ByteArrayContent(excelFile)
            //};

            //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = "tasks.xlsx"
            //};


            return response;
        }

        /// <summary>
        /// Retrieves all tasks along with user details.
        /// </summary>
        /// <returns>A response containing tasks with user data.</returns>
        [HttpGet]
        [Route("gettaskswithuserdetails")]
        public IHttpActionResult GetTasksWithUserDetails()
        {
            // Call the service method to get tasks with user details
            _objResponse = _objTaskService.GetTasksWithUserDetails();

            // If there's an error, return the error message
            if (_objResponse.IsError)
            {
                return BadRequest(_objResponse.Message);
            }

            // Otherwise, return the data (tasks with user details) and a success message
            return Ok(_objResponse.Data);
        }


        #endregion
    }
}
