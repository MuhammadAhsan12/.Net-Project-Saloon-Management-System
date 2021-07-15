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
    public class ProductController : Controller
    {
        SaloonDBEntities db = new SaloonDBEntities();
        // GET: Employees
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(string searchTxt)
        {
            var product = db.Products.ToList();
            if (searchTxt != null)
            {
                product = db.Products.Where(x => x.Product_Name.Contains(searchTxt)).ToList();
            }
            return View(product);

        }


        public ActionResult Create()
        {
            var categorylist = db.Product_Category.ToList();
            ViewBag.CategoryList = new SelectList(categorylist, "Prod_Category_Id", "Prod_Category_Name");
            return View();
        }


        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Product pro)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extention = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/imgs/"), _filename);
            pro.Product_Img = "~/Content/imgs/" + _filename;

            if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
            {
                if (file.ContentLength <= 1000000)
                {
                    db.Products.Add(pro);
                    if (db.SaveChanges() > 0)
                    {
                        file.SaveAs(path);
                        ViewBag.msg = "Product Added";
                        ModelState.Clear();
                    }
                }

                else
                {
                    ViewBag.msg = "Image Size is not Valid";
                }
            }

            var categorylist = db.Product_Category.ToList();
            ViewBag.CategoryList = new SelectList(categorylist, "Prod_Category_Id", "Prod_Category_Name");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            Session["imgpath"] = product.Product_Img;

            if (product == null)
            {
                return HttpNotFound();
            }

            var categorylist = db.Product_Category.ToList();
            ViewBag.CategoryList = new SelectList(categorylist, "Prod_Category_Id", "Prod_Category_Name");
            return View();
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Product pro)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extention = Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/imgs/"), _filename);
                    pro.Product_Img = "~/Content/imgs/" + _filename;

                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 1000000)
                        {
                            db.Entry(pro).State = EntityState.Modified;
                            string oldImgPath = Request.MapPath(Session["imgpath"].ToString());
                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);

                                if (System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }

                                TempData["Msg"] = "Product Updated";
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
               pro.Product_Img = Session["imgpath"].ToString();
                db.Entry(pro).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
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

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
           
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            string currentimg = Request.MapPath(product.Product_Img);
            db.Entry(product).State = EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentimg))
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