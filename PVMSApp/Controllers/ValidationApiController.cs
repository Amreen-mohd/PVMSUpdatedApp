using PVMSApp.Models.BL;
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
    public class ValidationApiController : ApiController
    {
        // GET api/<controller>
        

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
        [Route("api/Validation/GetStates")]
        public IHttpActionResult GetStates()
        {
            List<State> states = new Validation().validateState();
            return Ok(states);
        }

        [HttpGet]
        [Route("api/Validation/GetCities")]
        public IHttpActionResult GetCities(string sId)
        {
            List<city> cities = new Validation().validateCity(sId);
            return Ok(cities);
        }

        [HttpGet]
        [Route("api/Validation/GetVisaForms")]
        public IHttpActionResult GetVisaForms()
        {
            List<Visa_form> visaForms = new Validation().getvisa();
            return Ok(visaForms);
        }

        [HttpGet]
        [Route("api/Validation/GetVisaTypes")]
        public IHttpActionResult GetVisaTypes()
        {
            List<Visa_type> visaTypes = new Validation().getvisaType1();
            return Ok(visaTypes);
        }
    }
}