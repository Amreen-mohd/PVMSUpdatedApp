using PVMSApp.Models.BL;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
   
    public class VisaRegController : Controller
    {
        // GET: VisaReg
        Validation V = new Validation();
        public ActionResult VisaRegistration()
        { 
            ViewBag.vlist = V.getvisaType1();
            return View();
        }
        [HttpPost]
        public ActionResult VisaRegistration(visa v)
        {
            ViewBag.vlist = V.getvisaType1();
            if (ModelState.IsValid)
            {
                string R = V.insertvisa(v);
                if (R.Equals("success"))
                {
                    return RedirectToAction("Success", "VisaReg", v);
                }
                else
                {
                    ViewBag.msg = R;
                    return View();
                }

            }
           
            return View();
        }
        public ActionResult Success(visa v)
        {
            string id = v.visaId;
            visa L = V.getbyvisaid(id);

            return View(L);
        }
    }

}
