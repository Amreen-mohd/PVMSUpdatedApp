using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PVMSApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Demo()
        {
            List<string> qlist = new List<string>();
            string q = "what is your pet name";
            string q1 = "what is your favourite holiday spot";
            string q3 = "who is your favourite actor";
            string q4 = "what is your nationality";
            qlist.Add(q1);
            qlist.Add(q3);
            qlist.Add(q4);
            qlist.Add(q);
            ViewBag.qlist = qlist;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}
