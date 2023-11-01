using PVMSApp.Models.BL;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    public class PassportRenewalController : Controller
    {
            Validation V = new Validation();

        public ActionResult Renewal()
        {

            ViewBag.slist = V.validateState();
            string userId = Request.Cookies["userId"].Value;
            passport p = new passport();
            p.userId = userId;

            return View(p);
        }
        [HttpPost]
        public ActionResult Renewal(passport p)
        {
            List<city> cities = V.validateCity(p.state);
            ViewBag.slist = V.validateState();
            if (ModelState.IsValid)
            {
                string R = V.passportrenewal(p);
                if (R.Equals("success"))
                {
                    return RedirectToAction("Success", "PassportReg", new { P= p });
                }
                else
                {
                    ViewBag.msg = R;
                    return View();
                }
            }
            return View();
        }
        public JsonResult getcities(string sId)
        {
            List<city> cities = V.validateCity(sId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        
    }
}