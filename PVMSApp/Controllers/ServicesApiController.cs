using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PVMSApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ServicesApiController : ApiController
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

        [HttpGet]
        [Route("api/Services/UpdateStatus")]
        public IHttpActionResult UpdateStatus(string userId)
        {
            new DbOperations().updstatus();
            string passportId = new DbOperations().fetchCurrentPassportId(userId);
            List<string> visaId = new DbOperations().fetchCurrentVisaId(userId);
            List<string> result = new List<string>();
            if(passportId != null)
            {
                result.Add(passportId);
            }
            else
            {
                result.Add("N/A");
            }
            if(visaId != null)
            {
                for (int i = 0; i < visaId.Count; i++)
                {
                    result.Add(visaId[i]);
                }
            }
            else
            {
                result.Add("N/A");
            }
            return Ok(result);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Services/ApplyPassport")]
        public IHttpActionResult ApplyPassport(string userId)
        {
            passport p = new DbOperations().getuserById(userId);
            return Ok(p);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Services/ApplyVisa")]
        public IHttpActionResult ApplyVisa(string userId)
        {
            passport p = new DbOperations().getuserById(userId);
            return Ok(p);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Services/PassportRenewal")]
        public IHttpActionResult PassportRenewal(string userId)
        {
            List<passport> p = new DbOperations().getuserById1(userId);
            return Ok(p);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Services/VisaCancellation")]
        public IHttpActionResult VisaCancellation(string userId)
        {
            List<visa> p = new DbOperations().getuserbyvisaid1(userId);
            return Ok(p);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Services/GetUser")]
        public IHttpActionResult GetUser(string userId)
        {
            userProfile user = new DbOperations().fetchUserProfile(userId);
            return Ok(user);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Services/UpdateUser")]
        public IHttpActionResult UpdateUser(userProfile user)
        {
            new DbOperations().updateUserProfile(user);
            return Ok(user);
        }
        [HttpGet]
        [Route("api/Services/GetPassportId")]
        public IHttpActionResult GetPassportID(string userId)
        {
            PassportId p = new PassportId();
            
            p = new DbOperations().GetPassportId(userId);
            return Ok(p);
        }
    }
}