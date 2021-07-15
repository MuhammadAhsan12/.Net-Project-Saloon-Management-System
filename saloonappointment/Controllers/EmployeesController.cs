using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace saloonappointment.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        SaloonDBEntities db = new SaloonDBEntities();
        // GET: Employees
        public ActionResult Index()
        {
            var data = db.Employees.ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(string searchTxt)
        {
            var employee = db.Employees.ToList();
            if (searchTxt != null)
            {
                employee = db.Employees.Where(x => x.Emp_Name.Contains(searchTxt)).ToList();
            }
            return View(employee);

        }

        public ActionResult Create()
        {            
            return View();
        }


        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Employee emp)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extention = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/imgs/"), _filename);
            emp.Emp_Image = "~/Content/imgs/" + _filename;

            if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
            {
                if (file.ContentLength <= 1000000)
                {
                    db.Employees.Add(emp);
                    if (db.SaveChanges() > 0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "Record Added";
                        ModelState.Clear();
                    }
                }

                else
                {
                    ViewBag.msg = "Image Size is not Valid";
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.Employees.Find(id);
            Session["imgpath"] = employee.Emp_Image;

            if(employee == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Employee emp)
        {
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extention = Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/imgs/"), _filename);
                    emp.Emp_Image = "~/Content/imgs/" + _filename;

                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 1000000)
                        {
                            db.Entry(emp).State = EntityState.Modified;
                            string oldImgPath = Request.MapPath(Session["imgpath"].ToString());
                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);

                                if(System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }

                                TempData["Msg"] = "Record Updated";
                            }
                        }

                        else
                        {
                            ViewBag.msg = "Image Size is not Valid";
                        }
                    }

                }
            }

            else
            {
                emp.Emp_Image = Session["imgpath"].ToString();
                db.Entry(emp).State = EntityState.Modified;
                if(db.SaveChanges() > 0)
                {
                    TempData["msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.Employees.Find(id);       

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            string currentimg = Request.MapPath(employee.Emp_Image);
            db.Entry(employee).State = EntityState.Deleted;
            if(db.SaveChanges() > 0)
            {
                if(System.IO.File.Exists(currentimg))
                {
                    System.IO.File.Delete(currentimg);
                }
                TempData["msg"] = "Data Deleted";
                return RedirectToAction("Index");
            }


            return View();
        }
    }
}