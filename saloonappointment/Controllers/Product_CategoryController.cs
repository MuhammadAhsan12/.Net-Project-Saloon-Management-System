using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using saloonappointment;

namespace saloonappointment.Controllers
{
    [Authorize]
    public class Product_CategoryController : Controller
    {
        private SaloonDBEntities db = new SaloonDBEntities();

        // GET: Product_Category
        public ActionResult Index()
        {
            return View(db.Product_Category.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searchTxt)
        {
            var productcat = db.Product_Category.ToList();
            if (searchTxt != null)
            {
                productcat = db.Product_Category.Where(x => x.Prod_Category_Name.Contains(searchTxt)).ToList();
            }
            return View(productcat);

        }

        // GET: Product_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            return View(product_Category);
        }

        // GET: Product_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Prod_Category_Id,Prod_Category_Name")] Product_Category product_Category)
        {
            if (ModelState.IsValid)
            {
                db.Product_Category.Add(product_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Category);
        }

        // GET: Product_Category/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            return View(product_Category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Prod_Category_Id,Prod_Category_Name")] Product_Category product_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Category);
        }

        // GET: Product_Category/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            return View(product_Category);
        }

        // POST: Product_Category/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Category product_Category = db.Product_Category.Find(id);
            db.Product_Category.Remove(product_Category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
