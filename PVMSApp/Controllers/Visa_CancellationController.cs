using PVMSApp.Models.BL;
using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    public class Visa_CancellationController : Controller
    {
        // GET: Visa_Cancellation
        Validation V=new Validation();
        public ActionResult VisaCancellation()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult VisaCancellation(visa v)
        {
            string R = V.visacancellation(v);
            
                ViewBag.vlist=R;
           
         
            //if (ModelState.IsValid)
            //{
            //    string R = V.visacancellation(v);

            //    if (R.Equals("success"))
            //    {
            //        return RedirectToAction("Success", "Visa_Cancellation", v);
            //    }
            //    else
            //    {
            //        ViewBag.msg = R;
            //        return View();
            //    }

            //}
            return View();
        }
        //public ActionResult Success(visa v)
        //{
        //    string id = v.visaId;
        //    List<visa> L = V.getbyvisaid(id);

        //    return View(L);
        //}
        public ActionResult demo()
        {
            return View();
        }
    }
}