using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Organizer.Controllers
{
    public class AppointmentController : Controller
    {
        private OrganizerDbContext db = new OrganizerDbContext();
        // GET: Appointment
        public ActionResult ListAppointment()
        {
            List<Appointment> appointments = db.Appointments
                .Include(a => a.Broker)
                .Include(a => a.Customer)
                .ToList();
            return View(appointments);
        }
        public ActionResult AddAppointment()
        {
            ViewBag.BrokerID = new SelectList(db.Brokers, "BrokerID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment([Bind(Include = "AppointmentID, DateHour, BrokerID, CustomerID, Subject")] Appointment appointment)
        {
            var queryResult = db.Appointments.SingleOrDefault(a => (a.BrokerID == appointment.BrokerID && a.DateHour == appointment.DateHour) || (a.CustomerID == appointment.CustomerID && a.DateHour == appointment.DateHour));
            if (queryResult != null)
            {
                ModelState.AddModelError("DateHour", "le rendez vous est déjà pris");
            }
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrokerID = new SelectList(db.Brokers, "BrokerID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");

            return View();
        }
        public ActionResult DetailsAppointment(int? id)
        {
            ViewBag.BrokerID = new SelectList(db.Brokers, "BrokerID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointments = db.Appointments.Find(id);
            if (appointments == null)
            {
                return HttpNotFound();
            }
            return View(appointments);
        }
        public ActionResult EditAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrokerID = new SelectList(db.Brokers, "BrokerID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            return View(appointment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment([Bind(Include = "AppointmentID, DateHour, BrokerID, CustomerID, Subject")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.BrokerID = new SelectList(db.Brokers, "BrokerID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            return View("DetailsAppointment");
        }
        public ActionResult DeleteAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }
        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}