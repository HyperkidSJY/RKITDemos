using ORMTools.Converter;
using ORMTools.Models;
using ORMTools.Models.ENUMS;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Web;

namespace ORMTools.BusinessLogic
{
    public class BLStudent : IDataHandler<DTOSTU01>
    {
        private STU01 _objSTU01;
        private int _id;
        private readonly IDbConnectionFactory _dbFactory;
        private Response _objResponse;
        public EnmType Type { get; set; }

        public BLStudent()
        {
            _objResponse = new Response();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        public List<STU01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var students = db.Select<STU01>();
                return students;
            }
        }

        public STU01 Get(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var students = db.SingleById<STU01>(id);
                return students;
            }
        }

        private bool IsSTU01Exist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<STU01>(id);
            }
        }

        public Response Delete(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.DeleteById<STU01>(id);
                }
                _objResponse.Message = "Data Deleted";
            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        public void PreSave(DTOSTU01 objDto)
        {
            _objSTU01 = objDto.Convert<STU01>();
            if (Type == EnmType.E)
            {
                if (objDto.P01F01 > 0)
                {
                    _id = objDto.P01F01;
                }
            }
        }

        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Type == EnmType.A)
                    {
                        db.Insert(_objSTU01);
                        _objResponse.Message = "Data Added";
                    }
                    if (Type == EnmType.E)
                    {
                        db.Update(_objSTU01);
                        _objResponse.Message = "Data Updated";
                    }
                }

            }
            catch (Exception ex)
            {
                _objResponse.isError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        public Response Validate()
        {
            if (Type == EnmType.E)
            {
                if (!(_id > 0))
                {
                    _objResponse.isError = true;
                    _objResponse.Message = "Enter Correct Id";
                }
                else
                {
                    bool isSTU01 = IsSTU01Exist(_id);
                    if (!isSTU01)
                    {
                        _objResponse.isError = true;
                        _objResponse.Message = "User not Found";
                    }
                }
            }
            return _objResponse;
        }
    }
}