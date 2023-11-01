using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EmailApiController : ApiController
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Email/SendEmail")]
        public IHttpActionResult SendEmail(string email)
        {
            try
            {
                user_registration2 user = new DbOperations().fetchPassword(email);
                MimeMessage msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("sai", "kadarisaikrishna112@gmail.com"));
                msg.To.Add(MailboxAddress.Parse(email));
                msg.Subject = "Hello";
                msg.Body = new TextPart("plain")
                {
                    Text = "Hello " + user.firstName + "Your password is : " + user.password
                };

                SmtpClient smtp = new SmtpClient();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                smtp.CheckCertificateRevocation = false;
                smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate("kadarisaikrishna112@gmail.com", "qqyrplhmnhuqkuza");
                smtp.Send(msg);
                smtp.Disconnect(true);
                smtp.Dispose();
                return Ok();
            }
            catch (Exception ex)
            {
                return Json(HttpStatusCode.InternalServerError);
            }
        }
    }
}