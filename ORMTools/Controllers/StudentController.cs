using ORMTools.BusinessLogic;
using ORMTools.Converter;
using ORMTools.Models;
using ORMTools.Models.ENUMS;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ORMTools.Controllers
{
    #region "Student Controller"

    /// <summary>
    /// Controller for managing student-related operations such as adding, updating, deleting, and retrieving student records.
    /// </summary>
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private BLStudent _objBLStudent;
        private Response _objResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class.
        /// </summary>
        public StudentController()
        {
            _objBLStudent = new BLStudent();
        }

        /// <summary>
        /// Retrieves a list of all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        [Route("GetAllStudents")]
        public IHttpActionResult GetAllEmployee()
        {
            return Ok(_objBLStudent.GetAll());
        }

        /// <summary>
        /// Retrieves a student by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <returns>The student data.</returns>
        [HttpGet]
        [Route("GetStudentInfo/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            return Ok(_objBLStudent.Get(id));
        }

        /// <summary>
        /// Adds a new student to the database.
        /// </summary>
        /// <param name="student">The student data to be added.</param>
        /// <returns>A response indicating whether the student was added successfully or not.</returns>
        [HttpPost]
        [Route("AddStudent")]
        public IHttpActionResult AddNewEmployee(DTOYMU01 student)
        {
            _objBLStudent.Type = EnmType.A;
            _objBLStudent.PreSave(student);
            _objResponse = _objBLStudent.Validate();
            if (!_objResponse.isError)
            {
                _objResponse = _objBLStudent.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing student's data.
        /// </summary>
        /// <param name="student">The updated student data.</param>
        /// <returns>A response indicating whether the student was updated successfully or not.</returns>
        [HttpPut]
        [Route("UpdateStudent")]
        public IHttpActionResult UpdateEmployeeData(DTOYMU01 student)
        {
            _objBLStudent.Type = EnmType.E;
            _objBLStudent.PreSave(student);
            _objResponse = _objBLStudent.Validate();
            if (!_objResponse.isError)
            {
                return Ok(_objBLStudent.Save());
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes a student by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the student to be deleted.</param>
        /// <returns>A response indicating whether the student was deleted successfully or not.</returns>
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _objBLStudent.PreDelete(id);

            _objResponse = _objBLStudent.Validate();
            if (!_objResponse.isError)
            {
                return Ok(_objBLStudent.Delete());
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Searches for students by their name.
        /// </summary>
        /// <param name="name">The name of the student to search for.</param>
        /// <returns>A response containing the search results.</returns>
        [HttpGet]
        [Route("SearchStudentsByName/{name}")]
        public IHttpActionResult SearchStudentsByName(string name)
        {
            _objResponse = _objBLStudent.SearchByName(name);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves students belonging to a specific department.
        /// </summary>
        /// <param name="department">The department to filter students by.</param>
        /// <returns>A response containing the students in the specified department.</returns>
        [HttpGet]
        [Route("GetStudentsByDept/{department}")]
        public IHttpActionResult GetStudentsByDept(string department)
        {
            _objResponse = _objBLStudent.GetByDepartment(department);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves the total count of students in the system.
        /// </summary>
        /// <returns>A response containing the total student count.</returns>
        [HttpGet]
        [Route("GetTotalStudentCount")]
        public IHttpActionResult GetTotalStudentCount()
        {
            _objResponse = _objBLStudent.GetTotalCount();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves a list of students sorted by their name.
        /// </summary>
        /// <param name="ascending">Indicates whether the students should be sorted in ascending order (true) or descending order (false).</param>
        /// <returns>A response containing the sorted list of students.</returns>
        [HttpGet]
        [Route("GetStudentsSortedByName/{ascending}")]
        public IHttpActionResult GetStudentsSortedByName(bool ascending)
        {
            _objResponse = _objBLStudent.GetSortedByName(ascending);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates a student's semester using UpdateOnly method from BL.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <param name="semester">The new semester value to be updated.</param>
        /// <returns>A response indicating whether the semester was updated successfully or not.</returns>
        [HttpPut]
        [Route("UpdateStudentSemester/{id}")]
        public IHttpActionResult UpdateStudentSemester(int id, [FromBody] int semester)
        {
            _objResponse = _objBLStudent.UpdateSemester(id, semester);

            return Ok(_objResponse);
        }

        /// <summary>
        /// Gets  student's by department.
        /// </summary>
        [HttpPost]
        [Route("GetByDepartments")]
        public IHttpActionResult GetByDepartments([FromBody] List<string> departments)
        {
            // Call the BLStudent method to search by departments
            _objResponse = _objBLStudent.GetByDepartments(departments);

            // Return the response
            return Ok(_objResponse);
        }

        /// <summary>
        /// Add multiple students at once
        /// </summary>
        [HttpPost]
        [Route("AddMultipleStudents")]
        public IHttpActionResult AddMultipleStudents(List<DTOYMU01> students)
        {
            List<YMU01> studentList = students.Select(s => s.Convert<YMU01>()).ToList();
            _objResponse = _objBLStudent.AddAll(studentList);
            return Ok(_objResponse);
        }

        // InsertOnly: Insert specific fields of a student
        [HttpPost]
        [Route("InsertOnlyStudent")]
        public IHttpActionResult InsertOnlyStudent(DTOYMU01 student)
        {
            _objBLStudent.Type = EnmType.A;
            _objBLStudent.PreSave(student);
            _objResponse = _objBLStudent.InsertOnly(student);
            return Ok(_objResponse);
        }

    }

    #endregion "Student Controller"
}
