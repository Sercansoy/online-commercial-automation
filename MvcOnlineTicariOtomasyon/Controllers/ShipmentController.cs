using MvcOnlineTicariOtomasyon.Models.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ShipmentController : Controller
    {
        // GET: Shipment
        Context db = new Context();
        public ActionResult Index(string p)
        {
            var cargo = from x in db.Shipments select x;
            if (!string.IsNullOrEmpty(p))
            {
                cargo = cargo.Where(y=>y.TrackingCode.Contains(p));
            }
            return View(cargo.ToList());
        }

        [HttpGet]
        public ActionResult NewShipment()
        {
            ViewBag.TrackingCode = GenerateTrackingCode(10);
            return View();
        }

        [HttpPost]
        public ActionResult NewShipment(Shipment shipment)
        {
            db.Shipments.Add(shipment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CargoTracking(string id)
        {
            var value = db.ShipmentTrackings.Where(x=>x.TrackingCode == id).ToList();
            return View(value);
        }

        private static readonly Random random = new Random();

        private string GenerateTrackingCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            lock (random)
            {
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

    }
}