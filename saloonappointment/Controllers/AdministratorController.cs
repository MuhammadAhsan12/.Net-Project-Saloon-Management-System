using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace saloonappointment.Controllers
{
    public class AdministratorController : Controller
    {
        SaloonDBEntities db = new SaloonDBEntities();

        // GET: Administrator
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin s)
        {
            if(ModelState.IsValid == true)
            {
                var creadintials = db.Admins.Where(model => model.Email == s.Email && model.Password == s.Password).FirstOrDefault();
                if(creadintials == null)
                {
                    ViewBag.ErrorMessage = "Login Failed";
                    return View();
                }
                else
                {
                    Session["username"] = s.Name;
                    Session["Email"] = s.Email;
                    return RedirectToAction("Index", "Employees");
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login","Administrator");
        }

    }
}