using System.Web.Http;
using TaskManagement.Helpers;
using TaskManagement.Models;
using TaskManagement.Models.DTO;
using TaskManagement.Models.ENUM;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    /// <summary>
    /// Controller for managing users. Provides APIs for adding, updating, and deleting users.
    /// </summary>
    [RoutePrefix("api/users")]
    [JWTAuthorizor("Manager")]  // Ensures only users with the "Manager" role can access these APIs
    public class UserController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// The service class to handle user data operations.
        /// </summary>
        private UserService _objUserService;

        /// <summary>
        /// The response object to return the result of the API operations.
        /// </summary>
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the UserService instance.
        /// </summary>
        public UserController()
        {
            _objUserService = new UserService();
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="newUser">The user data to add.</param>
        /// <returns>The result of the user addition operation.</returns>
        [HttpPost]
        [Route("adduser")]
        public Response AddNewUser(DTOUSR01 newUser)
        {
            _objUserService.Type = EnmType.A;  // Set the operation type to Add
            _objUserService.PreSave(newUser);   // Prepare the user data for saving
            _objResponse = _objUserService.Validate();  // Validate the input

            if (!_objResponse.IsError)
            {
                _objResponse = _objUserService.Save();  // Save the user if no validation errors
            }

            return _objResponse;
        }

        [HttpPut]
        [Route("updateuser")]
        public Response UpdateUser(DTOUSR01 newUser)
        {
            _objUserService.Type = EnmType.E;
            _objUserService.PreSave(newUser);
            _objResponse = _objUserService.Validate();
            if (!_objResponse.IsError)
            {
                _objResponse = _objUserService.Save();
            }
            return _objResponse;
        }

        /// <summary>
        /// Deletes an existing user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete]
        [Route("deleteuser/{id}")]
        public Response DeleteUser(int id)
        {
            _objUserService.Type = EnmType.D;  // Set the operation type to Delete
            _objUserService.PreDelete(id);  // Prepare the user for deletion
            _objResponse = _objUserService.Validate();  // Validate the delete operation

            if (!_objResponse.IsError)
            {
                _objResponse = _objUserService.Delete();  // Delete the user if no validation errors
            }

            return _objResponse;
        }

        #endregion
    }
}
