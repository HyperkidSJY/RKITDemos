using CRUDDemo.BusinessLogic;
using CRUDDemo.Models;
using CRUDDemo.Models.DTO;
using System.Web.Http;
using CRUDDemo.Models.Enum;

namespace CRUDDemo.Controllers
{
    /// <summary>
    /// API controller for handling student-related operations.
    /// </summary>
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        private StudentServiceImpl _objofStudentService;
        private Response response;

        /// <summary>
        /// Constructor that initializes the StudentService implementation.
        /// </summary>
        public StudentController()
        {
            _objofStudentService = new StudentServiceImpl();
        }

        /// <summary>
        /// Retrieves a list of all students.
        /// </summary>
        /// <returns>An IHttpActionResult containing a list of students.</returns>
        [HttpGet]
        [Route("GetAllStudents")]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(_objofStudentService.GetAll());
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="student">The student data to be added.</param>
        /// <returns>A Response object indicating the result of the operation.</returns>
        [HttpPost]
        [Route("AddNewStudent")]
        public Response AddNewStudent(DTOSTU01 student)
        {
            _objofStudentService.type = EnmType.A;
            _objofStudentService.PreSave(student);
            response = _objofStudentService.Validate();

            // If no validation errors, save the student
            if (!response.IsError)
            {
                response = _objofStudentService.Save();
            }
            return response;
        }

        /// <summary>
        /// Updates an existing student's information.
        /// </summary>
        /// <param name="student">The student data to be updated.</param>
        /// <returns>A Response object indicating the result of the operation.</returns>
        [HttpPut]
        [Route("UpdateStudent")]
        public Response UpdateStudent(DTOSTU01 student)
        {
            _objofStudentService.type = EnmType.E;
            _objofStudentService.PreSave(student);
            response = _objofStudentService.Validate();

            // If no validation errors, save the updated student
            if (!response.IsError)
            {
                response = _objofStudentService.Save();
            }
            return response;
        }

        /// <summary>
        /// Deletes an existing student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>A Response object indicating the result of the operation.</returns>
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public Response DeleteStudent(int id)
        {
            _objofStudentService.type = EnmType.D;
            _objofStudentService.PreDelete(id);
            response = _objofStudentService.Validate();

            // If no validation errors, delete the student
            if (!response.IsError)
            {
                response = _objofStudentService.Delete();
            }
            return response;
        }
    }
}
