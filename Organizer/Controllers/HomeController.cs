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
    public class HomeController : Controller
    {
        private OrganizerDbContext db = new OrganizerDbContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewData["date"] = DateTime.Now;
            // (db.Brokers.ToList())
            return View();
        }
    }
}