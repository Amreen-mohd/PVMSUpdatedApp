using PVMSApp.Models.BL;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class VisaApiController : ApiController
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
        [System.Web.Http.Route("api/VisaReg/Visa")]
        public IHttpActionResult Post(visa v)
        {
            string res = new Validation().insertvisa(v);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/VisaReg/Success")]
        public IHttpActionResult VisaSuccess(string res)
        {
            visa visa = new Validation().getbyvisaid(res);
            return Ok(visa);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/VisaCancellation/CancelVisa")]
        public IHttpActionResult VisaCancellation(visa v)
        {
            string res = new Validation().visacancellation(v);
            string R = 
            "Your request has been submitted successfully."+res+
            " \n.Please pay to complete the cancellation process";
            return Ok(R);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/VisaCancellation/GetCountries")]
        public IHttpActionResult GetActiveVisaCountries(string res)
        {
            List<string> list = new Validation().ActiveVisaCountries(res);
            return Ok(list);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/VisaCancellation/GetVisaId")]
        public IHttpActionResult GetVisaIdbyCountry(string res)
        {
               string vid = new Validation().GetVisaIdByCountry(res);
            return Ok(vid);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/VisaCancellation/GetVisaCountryValidation")]
        public IHttpActionResult GetVisaCountryValidation(visa v)
        {
            string vid = new Validation().CountryVisaValidation(v);
            return Ok(vid);
        }

    }
}