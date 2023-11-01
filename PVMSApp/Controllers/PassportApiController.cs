using PVMSApp.Models.BL;
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
    public class PassportApiController : ApiController
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
        [System.Web.Http.Route("api/PassportReg/Passport")]
        public IHttpActionResult Post(passport p)
        {
            string R = new Validation().validatePassport(p);
            if (R.Equals("success"))
            {
                return Ok(R);
            }
            else
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PassportReg/Success")]
        public IHttpActionResult PassportSuccess(passport p)
        {
            string id = p.userId;
            passport passport = new Validation().getuserById(id);
            return Ok(passport);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PassportRenewal/Renewal")]
        public IHttpActionResult PassportRenewal(passport p)
        {
            string R = new Validation().passportrenewal(p);
            if (R.Equals("success"))
            {
                return Ok(R);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}