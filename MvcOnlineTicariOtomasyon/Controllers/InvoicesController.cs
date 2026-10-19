using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class InvoicesController : Controller
    {
        Context db = new Context();

        // GET: Invoices
        public ActionResult Index()
        {
            var value = db.Invoices.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult AddInvoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddInvoice(Invoice i)
        {
            db.Invoices.Add(i);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FindInvoice(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var invoice = db.Invoices.Find(id.Value);

            if (invoice == null)
            {
                return RedirectToAction("Index");
            }

            return View("FindInvoice", invoice);
        }

        [HttpPost]
        public ActionResult FindInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
                return View("FindInvoice", invoice);

            var i = db.Invoices.Find(invoice.InvoiceId);

            if (i == null)
                return RedirectToAction("Index");

            i.InvoiceSerialNumber = invoice.InvoiceSerialNumber;
            i.InvoiceSequenceNumber = invoice.InvoiceSequenceNumber;
            i.Date = invoice.Date;
            i.Time = invoice.Time;
            i.TaxOffice = invoice.TaxOffice;
            i.DeliveredBy = invoice.DeliveredBy;
            i.ReceivedBy = invoice.ReceivedBy;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailInvoice(int id)
        {

            var value = db.InvoiceItems.Where(x => x.Invoiceid == id).ToList();
            var InvoiceNum = db.Invoices.Where(x => x.InvoiceId == id).Select(i => i.InvoiceSequenceNumber).FirstOrDefault();
            ViewBag.InvoiceId = id;
            ViewBag.InvoiceNum = InvoiceNum;
            return View(value);
        }
        [HttpGet]
        public ActionResult NewItem(int invoiceId)
        {
            var item = new InvoiceItem
            {
                Invoiceid = invoiceId  
            };
            return View(item);
        }
        [HttpPost]
        public ActionResult NewItem(InvoiceItem i)
        {
            db.InvoiceItems.Add(i);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceItems()
        {
            var model = new InvoiceTg();
            model.Invoices = db.Invoices.Include(x => x.InvoiceItems).ToList();
            return View(model);
        }

        public ActionResult SaveInvoice (string InvoiceSerialNumber, string InvoiceSequenceNumber, string Date,
            string TaxOffice, string Time, string DeliveredBy, string ReceivedBy, string Total, InvoiceItem[] items)
        {
            DateTime parsedDate;
            if(!DateTime.TryParseExact(Date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out parsedDate))
            {
                return Json("Invalid Date Format.", JsonRequestBehavior.AllowGet);
            }

            Invoice x = new Invoice();
            x.InvoiceSerialNumber = InvoiceSerialNumber;
            x.InvoiceSequenceNumber = InvoiceSequenceNumber;
            x.Date = parsedDate;
            x.TaxOffice = TaxOffice;
            x.Time = Time;
            x.DeliveredBy = DeliveredBy;
            x.ReceivedBy = ReceivedBy;
            //x.Total = decimal.Parse(Total);
            db.Invoices.Add(x);
            foreach(var y in  items)
            {
                InvoiceItem z = new InvoiceItem();
                z.Description = y.Description;
                z.UnitPrice = y.UnitPrice;
                z.Quantity = y.Quantity;
                z.Invoiceid = y.InvoiceItemId;
                z.TotalAmount = y.TotalAmount;
                db.InvoiceItems.Add(z);
            }
            db.SaveChanges();
            return Json("The transaction was completed successfully.", JsonRequestBehavior.AllowGet);


        }
    }
}