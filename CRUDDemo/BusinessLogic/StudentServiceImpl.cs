using CRUDDemo.Helper;
using CRUDDemo.Models;
using CRUDDemo.Models.DTO;
using CRUDDemo.Models.Enum;
using CRUDDemo.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace CRUDDemo.BusinessLogic
{
    /// <summary>
    /// Implements business logic for handling student-related operations.
    /// </summary>
    public class StudentServiceImpl : IStudentSerivce<DTOSTU01>
    {
        private string _connectionString;
        private STU01 _objSTU01;
        private Response _objResponse;
        private int _id;

        /// <summary>
        /// Specifies the type of operation (Add, Edit, Delete).
        /// </summary>
        public EnmType type { get; set; }

        /// <summary>
        /// Constructor that initializes the service and sets up the database connection string.
        /// </summary>
        public StudentServiceImpl()
        {
            _objResponse = new Response();
            _connectionString = HttpContext.Current.Application["ConnectionString"] as string;
        }

        /// <summary>
        /// Retrieves a list of all students from the database.
        /// </summary>
        /// <returns>A list of student objects.</returns>
        public List<STU01> GetAll()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            List<STU01> studentList = new List<STU01>();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM STU01;", connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentList.Add(new STU01()
                            {
                                P01F01 = (int)reader[0],
                                P01F02 = (string)reader[1],
                                P01F03 = (string)reader[2],
                                P01F04 = (int)reader[3],
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return studentList;
        }

        /// <summary>
        /// Prepares for the deletion of a student by verifying if the student exists.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        public void PreDelete(int id)
        {
            if (IsSTU01Exist(id))
            {
                _id = id;
            }
        }

        /// <summary>
        /// Prepares the student data for saving or updating.
        /// </summary>
        /// <param name="objDTO">The student data transfer object (DTO) to be saved or updated.</param>
        public void PreSave(DTOSTU01 objDTO)
        {
            _objSTU01 = objDTO.Convert<STU01>();
            if (type == EnmType.E)
            {
                if (objDTO.P01F01 > 0)
                {
                    _id = objDTO.P01F01;
                }
            }
        }

        /// <summary>
        /// Saves or updates a student based on the operation type.
        /// </summary>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        public Response Save()
        {
            if (type == EnmType.E)
            {
                return Update(_id, _objSTU01);
            }
            if (type == EnmType.A)
            {
                return Add(_objSTU01);
            }
            return _objResponse;
        }

        /// <summary>
        /// Validates the student data before performing any operation.
        /// </summary>
        /// <returns>A Response object indicating validation results.</returns>
        public Response Validate()
        {
            if (type == EnmType.E || type == EnmType.D)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct id";
                }
            }
            return _objResponse;
        }

        /// <summary>
        /// Updates the details of an existing student.
        /// </summary>
        /// <param name="id">The ID of the student to be updated.</param>
        /// <param name="student">The student object containing updated information.</param>
        /// <returns>A Response object indicating the success or failure of the update operation.</returns>
        public Response Update(int id, STU01 student)
        {
            if (!IsSTU01Exist(id))
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Student with the provided ID does not exist.";
                return _objResponse;
            }

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE STU01 " +
                                        "SET P01F02 = @Name, " +
                                        "P01F03 = @Dept, " +
                                        "P01F04 = @Sem " +
                                        "WHERE P01F01 = @Id ";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", student.P01F02);
                    cmd.Parameters.AddWithValue("@Dept", student.P01F03);
                    cmd.Parameters.AddWithValue("@Sem", student.P01F04);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
                return _objResponse;
            }
            finally { connection.Close(); }

            _objResponse.Message = "Data Update";
            return _objResponse;
        }

        /// <summary>
        /// Adds a new student to the database.
        /// </summary>
        /// <param name="student">The student object to be added.</param>
        /// <returns>A Response object indicating the success or failure of the add operation.</returns>
        public Response Add(STU01 student)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO STU01 (P01F02, P01F03, P01F04) VALUES (@Name, @Dept, @Sem)";

                    cmd.Parameters.AddWithValue("@Name", student.P01F02);
                    cmd.Parameters.AddWithValue("@Dept", student.P01F03);
                    cmd.Parameters.AddWithValue("@Sem", student.P01F04);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
                return _objResponse;
            }
            finally { connection.Close(); }
            _objResponse.Message = "Data Added";
            return _objResponse;
        }

        /// <summary>
        /// Deletes a student from the database.
        /// </summary>
        /// <returns>A Response object indicating the success or failure of the delete operation.</returns>
        public Response Delete()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM STU01 " +
                                        "WHERE P01F01 = @Id";

                    cmd.Parameters.AddWithValue("@Id", _id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
                return _objResponse;
            }
            finally { connection.Close(); }
            _objResponse.Message = "Data Deleted";
            return _objResponse;
        }

        /// <summary>
        /// Checks if a student with the given ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the student to check.</param>
        /// <returns>True if the student exists, otherwise false.</returns>
        private bool IsSTU01Exist(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM STU01 WHERE P01F01 = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
