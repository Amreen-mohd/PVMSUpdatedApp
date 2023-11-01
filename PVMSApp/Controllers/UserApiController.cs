using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/AddUser")]
        public IHttpActionResult Post(user_registration2 user)
        {
            user_registration2 u = new DbOperations().insert(user);
            return Ok(u);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/LoginUser")]
        public IHttpActionResult Post(userLogin userLogin)
        {
            string userId = userLogin.userId;
            string contactNo = userLogin.contactNo;
            string password = userLogin.password;
            bool res = new DbOperations().checkUser(userId, contactNo, password);
            if(res.Equals(true))
            {
                string result = "success";
                return Ok(result);
            }
            else
            {
                return InternalServerError();
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/User/GetContact")]
        public IHttpActionResult Getcontact(string res)
        {
            string r= new DbOperations().verifyphone(res);
            return Ok(r);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/User/GetEmail")]
        public IHttpActionResult GetEmail(string res)
        {
            string r = new DbOperations().verifyemail(res);
            return Ok(r);
        }
    }
}