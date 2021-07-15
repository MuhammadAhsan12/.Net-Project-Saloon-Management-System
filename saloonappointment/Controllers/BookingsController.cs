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
    public class BookingsController : Controller
    {
        private SaloonDBEntities db = new SaloonDBEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Booking1).Include(b => b.Booking2);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name");
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Booking_Id,Booking_Name,Qty,Booking_Date,Advance,Total_Amount,Client_Id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name", booking.Booking_Id);
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name", booking.Booking_Id);
            return View(booking);
        }

        // GET: Bookings/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name", booking.Booking_Id);
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name", booking.Booking_Id);
            return View(booking);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_Id,Booking_Name,Qty,Booking_Date,Advance,Total_Amount,Client_Id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name", booking.Booking_Id);
            ViewBag.Booking_Id = new SelectList(db.Bookings, "Booking_Id", "Booking_Name", booking.Booking_Id);
            return View(booking);
        }

        // GET: Bookings/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
