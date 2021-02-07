using APIAngular.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Security.Claims;

namespace APIAngular.Controllers
{
    public class StudentsController : ApiController
    {
        private StudentDB DB;

        public StudentsController()
        {
            DB = new StudentDB();
        }

        [Route("GetStudents")]
        [HttpGet]
        public IHttpActionResult GetStudents()
        {
            //Thread.Sleep(4000);
            //return Ok(DB.Students.Join(DB.Addresses, m => m.AddressId, n => n.AddressId, (m, n) => new { Student = m, Address = n }).ToList());
            var data = DB.Students.ToList();
            return Ok(data);
            //return InternalServerError(new Exception("Some Error Occured"));
        }

        [Route("GetStudent/{id}")]
        [HttpGet]
        public IHttpActionResult GetStudentDetails(int id)
        {
            //Thread.Sleep(2000);
            //return Ok(DB.Students.Join(DB.Addresses, m => m.AddressId, n => n.AddressId, (m, n) => new { Student = m, Address = n }).ToList());
            var data = DB.Students.FirstOrDefault(m => m.StudentId == id);
            return Ok(data);
        }

        [Route("AddStudent")]
        [HttpPost]
        public IHttpActionResult AddStudent([FromBody]Student student)
        {
            DB.Students.Add(student);
            DB.SaveChanges();
            return Ok();
        }

        [Route("UpdateStudent")]
        [HttpPut]
        public IHttpActionResult UpdateStudent([FromBody]Student student)
        {
            var std = DB.Students.FirstOrDefault(m => m.StudentId == student.StudentId);
            std.FirstName = student.FirstName;
            std.LastName = student.LastName;
            std.MobileNo = student.MobileNo;
            std.EmailId = student.EmailId;

            var address = DB.Addresses.FirstOrDefault(m=>m.AddressId==std.AddressId);
            address.AddLine1 = student.Address.AddLine1;
            address.AddLine2 = student.Address.AddLine2;
            address.AddLine3 = student.Address.AddLine3;
            address.City = student.Address.City;
            address.State = student.Address.State;
            DB.SaveChanges();
            return Ok();
        }

        [Route("DeleteStudent/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            var std = DB.Students.FirstOrDefault(m => m.StudentId == id);
            DB.Addresses.Remove(DB.Addresses.FirstOrDefault(m => m.AddressId == std.AddressId));
            DB.Students.Remove(std);            
            DB.SaveChanges();
            return Ok();
        }

    }
}
