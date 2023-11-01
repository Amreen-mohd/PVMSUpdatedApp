using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    public class Customer_RegistrationController : Controller
    {
        // GET: Customer_Registration
        public ActionResult Index()
        {
            user_registration u=new user_registration();
            return View(u);
        }
        [HttpPost]
        public ActionResult Index(user_registration U)
        {
            DbOperations D=new DbOperations();
            D.insert(U);
            return View();
        }
    }
}