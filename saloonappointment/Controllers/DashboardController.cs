using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace saloonappointment.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
           if(Session["Email"] == null)
            {
                return RedirectToAction("Login", "Administrator");
            }
            return View();
        }
    }
}