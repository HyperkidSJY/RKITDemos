using Google.Protobuf.Compiler;
using Org.BouncyCastle.Utilities;
using ORMTools.Converter;
using ORMTools.Models;
using ORMTools.Models.ENUMS;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORMTools.BusinessLogic
{
    #region "BLStudent Class"

    /// <summary>
    /// Business Logic class for handling student-related operations.
    /// Implements the IDataHandler interface for DTOYMU01.
    /// </summary>
    public class BLStudent : IDataHandler<DTOYMU01>
    {
        #region "Private Members"

        private YMU01 _objSTU01;
        private int _id;
        private readonly IDbConnectionFactory _dbFactory;
        private Response _objResponse;

        #endregion "Private Members"

        #region "Properties"

        /// <summary>
        /// Gets or sets the type of operation (e.g., Add, Edit, Delete).
        /// </summary>
        public EnmType Type { get; set; }

        #endregion "Properties"

        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BLStudent"/> class.
        /// </summary>
        public BLStudent()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        #endregion "Constructor"

        #region "CRUD Methods"

        /// <summary>
        /// Gets all student records from the database.
        /// </summary>
        /// <returns>A list of all students.</returns>
        public List<YMU01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var students = db.Select<YMU01>();
                return students;
            }
        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns>The student record.</returns>
        public YMU01 Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var students = db.SingleById<YMU01>(id);
                return students;
            }
        }

        #endregion "CRUD Methods"

        #region "Helper Methods"

        /// <summary>
        /// Checks if a student exists by their ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns>True if the student exists; otherwise, false.</returns>
        private bool IsSTU01Exist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<YMU01>(id);
            }
        }

        #endregion "Helper Methods"

        #region "Save and Delete Operations"

        /// <summary>
        /// Deletes a student record.
        /// </summary>
        /// <returns>A response indicating success or failure.</returns>
        public Response Delete()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.DeleteById<YMU01>(_id);
                    _objResponse.isError = false;
                    _objResponse.Message = "User Deleted";
                }
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Prepares the object for saving.
        /// </summary>
        /// <param name="objDto">The data transfer object (DTO) to be converted.</param>
        public void PreSave(DTOYMU01 objDto)
        {
            _objSTU01 = objDto.Convert<YMU01>();
            if (Type == EnmType.E)
            {
                if (IsSTU01Exist(objDto.U01F01))
                {
                    _id = objDto.U01F01;
                }
            }
        }

        /// <summary>
        /// Saves the object depending on the operation type.
        /// </summary>
        /// <returns>A response indicating success or failure.</returns>
        public Response Save()
        {
            if (Type == EnmType.A)
            {
                Add(_objSTU01);
            }
            if (Type == EnmType.E)
            {
                Update(_objSTU01);
            }
            return _objResponse;
        }

        /// <summary>
        /// Adds a new student record.
        /// </summary>
        /// <param name="user">The student to be added.</param>
        /// <returns>A response indicating success or failure.</returns>
        public Response Add(YMU01 user)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(user);
                }
                _objResponse.Data = user;
                _objResponse.Message = "User Added";
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Updates an existing student record.
        /// </summary>
        /// <param name="user">The student to be updated.</param>
        public void Update(YMU01 user)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Update(user);
                }
                _objResponse.Data = new { User = user };
                _objResponse.Message = "User Updated";
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
        }

        /// <summary>
        /// Updates the student's semester using UpdateOnly.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <param name="semester">The new semester value.</param>
        /// <returns>A response indicating success or failure.</returns>
        public Response UpdateSemester(int studentId, int newSemester)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.UpdateOnly<YMU01>(
                        () => new YMU01 { U01F04 = newSemester }, // Specify the value to update (semester)
                      where: x => x.U01F01 == studentId               // Condition to match the student by ID
                    );

                }

                _objResponse.Data = new { StudentId = studentId, Semester = newSemester };
                _objResponse.Message = "Semester updated successfully.";
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }

            return _objResponse;
        }

        #endregion "Save and Delete Operations"

        #region "Validation"

        /// <summary>
        /// Validates the object before saving or deleting.
        /// </summary>
        /// <returns>A response indicating validation result.</returns>
        public Response Validate()
        {
            if (Type == EnmType.E || Type == EnmType.D)
            {
                if (!(_id > 0))
                {
                    _objResponse.isError = true;
                    _objResponse.Message = "Enter Correct ID";
                }
            }
            return _objResponse;
        }

        /// <summary>
        /// Prepares for deletion by verifying the existence of the student.
        /// </summary>
        /// <param name="id">The student ID.</param>
        public void PreDelete(int id)
        {
            if (IsSTU01Exist(id))
            {
                _id = id;
            }
        }

        #endregion "Validation"

        #region "Additional Search Operations"

        /// <summary>
        /// Searches for students by their name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A response with the result of the search.</returns>
        public Response SearchByName(string name)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<YMU01> students = db.Select<YMU01>(x => x.U01F02.Contains(name));
                    _objResponse.Data = students;
                    _objResponse.Message = students.Any() ? "Students found" : "No students found";
                    _objResponse.isError = false;
                }
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Retrieves students by their department.
        /// </summary>
        /// <param name="department">The department to filter students by.</param>
        /// <returns>A response with the students from the specified department.</returns>
        public Response GetByDepartment(string department)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<YMU01> students = db.Select<YMU01>(x => x.U01F03 == department);
                    _objResponse.Data = students;
                    _objResponse.Message = students.Any() ? "Students found" : "No students found";
                    _objResponse.isError = false;
                }
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Gets the total number of students.
        /// </summary>
        /// <returns>A response with the total student count.</returns>
        public Response GetTotalCount()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    long count = db.Scalar<long>("SELECT COUNT(*) FROM YMU01");

                    _objResponse.Data = count;
                    _objResponse.Message = $"Total student count: {count}";
                    _objResponse.isError = false;
                }
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Retrieves students sorted by name.
        /// </summary>
        /// <param name="ascending">True for ascending order, false for descending.</param>
        /// <returns>A response with the sorted list of students.</returns>
        public Response GetSortedByName(bool ascending)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<YMU01> students = ascending
                        ? db.Select<YMU01>().OrderBy(x => x.U01F02).ToList()
                        : db.Select<YMU01>().OrderByDescending(x => x.U01F02).ToList();
                    _objResponse.Data = students;
                    _objResponse.Message = students.Any() ? "Students sorted by name" : "No students found";
                    _objResponse.isError = false;
                }
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }


        /// <summary>
        /// Retrieves students by departments.
        /// </summary>

        public Response GetByDepartments(List<string> departments)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    var sqlExpression = db.From<YMU01>().Where(x => Sql.In(x.U01F03, departments));
                    List<YMU01> students = db.Select(sqlExpression);
                    _objResponse.Data = students;
                    _objResponse.Message = students.Any() ? "Students found" : "No students found";
                    _objResponse.isError = false;
                }
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }


        /// <summary>
        /// Add list of students.
        /// </summary>
        public Response AddAll(List<YMU01> students)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.InsertAll(students);
                }
                _objResponse.Data = students;
                _objResponse.Message = "Students Added";
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Add partial students.
        /// </summary>
        public Response InsertOnly(DTOYMU01 student)
        {
            try
            {   
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.InsertOnly(student, x => x.U01F02);
                }
                _objResponse.Data = student;
                _objResponse.Message = "Student Added with Only Selected Fields";
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        //db.UpdateNonDefaults(new Person { FirstName = "JJ" }, p => p.LastName == "Hendrix");
        //This method is designed to update the columns that have non-default values.
        //If the Person model has properties like FirstName, LastName, etc., and you pass an
        //object with some of these properties set to their default values(for example, null for strings, 0 for integers, false for booleans),
        //those properties won't be updated in the database.
         

        #endregion "Additional Search Operations"

    }

    #endregion "BLStudent Class"
}
