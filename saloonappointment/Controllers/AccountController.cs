using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using saloonappointment.Models;


namespace saloonappointment.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new SaloonDBEntities())
            {
                bool is_valid = context.Users.Any(x => x.U_Email == x.U_Email && x.U_Password == x.U_Password);
                if(is_valid)
                {
                    FormsAuthentication.SetAuthCookie(model.U_Firstname, false);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                   
                    ModelState.AddModelError("","Invalid Username and Password");
                    return View();
                }
            }
                
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var context = new SaloonDBEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }
    }
}