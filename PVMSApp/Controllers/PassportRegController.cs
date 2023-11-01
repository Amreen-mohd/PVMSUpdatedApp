using PVMSApp.Models.BL;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    public class PassportRegController : Controller
    {
        // GET: PassportReg
        Validation V = new Validation();
        
        public ActionResult PassportRegistration()
        {
            ViewBag.slist = V.validateState();
            //string userId = Request.Cookies["userId"].Value;
            passport p = new passport();
            //p.userId=userId;

            return View(p);
            
        }
        public JsonResult getcities(string sId)
        {
            List<city> cities = V.validateCity(sId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PassportRegistration(passport P)
        {
            List<city> cities = V.validateCity(P.state);
            ViewBag.slist = V.validateState();
            if (ModelState.IsValid)
            {                
                string R = V.validatePassport(P);
                if(R.Equals("success"))
                {
                    return RedirectToAction("Success","PassportReg",P);
                }
                else
                {
                    ViewBag.msg = R;
                    return View();
                }  
            }
            return View();
        }
        public ActionResult Success(passport P)
        {
            string id = P.userId;
            passport p = V.getuserById(id);

            return View(p);
        }
    }
}