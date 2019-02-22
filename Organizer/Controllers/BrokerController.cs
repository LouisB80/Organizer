using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Organizer.Controllers
{
    public class BrokerController : Controller
    {
        private OrganizerDbContext db = new OrganizerDbContext();
        // GET: Broker
        public ActionResult ListBrokers()
        {
            List<Broker> brokers = db.Brokers.ToList();
            return View(brokers);
        }
        // GET: Broker
        public ActionResult AddBroker()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBroker([Bind(Include = "Firstname, Lastname, Mail, PhoneNumber")] Broker broker)
        {
            if (ModelState.IsValid)
            {
                db.Brokers.Add(broker);
                db.SaveChanges();
                return RedirectToAction("ListBrokers");
            }
            return View();
        }
        public ActionResult ProfilBroker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker broker = db.Brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBroker([Bind(Include = "BrokerID, Firstname, Lastname, Mail, PhoneNumber")] Broker broker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListBrokers");
            }
            return View("ProfilBroker");
        }
        public ActionResult DeleteBroker(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker broker = db.Brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }        
            return View(broker);
        }
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker broker = db.Brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            db.Entry(broker).State = EntityState.Deleted;
            db.Brokers.Remove(broker);
            db.SaveChanges();
            return RedirectToAction("ListBrokers");
        }       
    }
}