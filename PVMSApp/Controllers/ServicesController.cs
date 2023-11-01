using PVMSApp.Models.DAO;
using PVMSApp.Models.BL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Security.Policy;

namespace PVMSApp.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services

        public ActionResult Index()
        {
            new DbOperations().updstatus();
            return View();
        }

        

        [HttpPost]
        public ActionResult Index(string btn)
        {
            string userId = Request.Cookies["userId"].Value;
            

            if (btn.Equals("APPLY PASSPORT"))
               
            {
                ViewBag.msg1 = null;
                ViewBag.msg2 = null;
                if (new DbOperations().getuserById(userId) != null)
                {
                    ViewBag.msg1 = "You Already Have a Valid Passport.";
                    ViewBag.msg2 = "You can apply for Paassport Renewal.";
                    return View();
                }

                return RedirectToAction("PassportRegistration","PassportReg");
            }
            else if (btn.Equals("APPLY VISA"))

            {
                ViewBag.msg1 = null;
                ViewBag.msg2 = null;
                if (new DbOperations().getuserById(userId) == null)
                {
                    ViewBag.msg1 = "You Don't have a valid Passport";
                    ViewBag.msg2 = "Apply a new one or renewal the old one.";

                    return View();
                }
                return RedirectToAction("VisaRegistration","VisaReg");
            }
            else if (btn.Equals("Passport Renewal"))
            {
                ViewBag.msg1 = null;
                ViewBag.msg2 = null;
                List<passport> lp = new DbOperations().getuserById1(userId);
                if ( lp.Count==0)
                {
                    ViewBag.msg1 = "You Don't have a valid Passport to Renew it.";
                    ViewBag.msg2="You can Apply for one";
                    return View();
                }
                return RedirectToAction("Renewal","PassportRenewal");
            }
            else if (btn.Equals("VISA Cancellation"))
            {
                ViewBag.msg1 = null;
                ViewBag.msg2 = null;
                List<visa> lv = new DbOperations().getuserbyvisaid1(userId);
                if (lv.Count>0)
                {
                    ViewBag.msg1 = "You Don't have a valid Visa to cancel";
                    ViewBag.msg2 = "Apply for a Visa";

                    return View();
                }
                return RedirectToAction("VisaCancellation","Visa_Cancellation");
            }
            return View();
        }

        public ActionResult UserProfile()
        {
            string userId = Request.Cookies["userId"].Value;
            user_registration2 user = new DbOperations().fetch(userId);
            return View(user);
        }

        [HttpPost]
        public ActionResult UserProfile(user_registration2 U)
        {
            U.userId = Request.Cookies["userId"].Value;
            new DbOperations().update(U);
            return View(U);
        }
        
    }
}