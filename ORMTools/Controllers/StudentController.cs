using ORMTools.BusinessLogic;
using ORMTools.Models;
using ORMTools.Models.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ORMTools.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private BLStudent _objBLStudent;
        private Response _objResponse;

        public StudentController()
        {
            _objBLStudent = new BLStudent();
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public IHttpActionResult GetAllEmployee()
        {
            return Ok(_objBLStudent.GetAll());
        }

        [HttpGet]
        [Route("GetStudentInfo/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            return Ok(_objBLStudent.Get(id));
        }

        [HttpPost]
        [Route("AddStudent")]
        public Response AddNewEmployee(DTOSTU01 student)
        {
            _objBLStudent.Type = EnmType.A;
            _objBLStudent.PreSave(student);
            _objResponse = _objBLStudent.Validate();
            if (!_objResponse.isError)
            {
                _objResponse = _objBLStudent.Save();
            }
            return _objResponse;
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public Response UpdateEmployeeData(DTOSTU01 student)
        {
            _objBLStudent.Type = EnmType.E;
            _objBLStudent.PreSave(student);
            _objResponse = _objBLStudent.Validate();
            if (!_objResponse.isError)
            {
                _objResponse = _objBLStudent.Save();
            }
            return _objResponse;
        }

        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public Response DeleteEmployee(int id)
        {
            return _objBLStudent.Delete(id);
        }
    }
}
