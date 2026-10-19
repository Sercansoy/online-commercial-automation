using MvcOnlineTicariOtomasyon.Models.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CustomerPanelController : Controller
    {
        // GET: CustomerPanel
        Context db = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = GetCustomerEmail();
            var value = db.Messages.Where(x => x.Receiver == mail).ToList();
            var mailID = db.Customers.Where(x=> x.CustomerEmail == mail).Select(y=>y.CustomerID).FirstOrDefault();
            var SaleHistory = db.SaleTransactions.Where(x=> x.Customerid == mailID).Count();
            var TotalSpent = db.SaleTransactions.Where(x=> x.Customerid == mailID).Sum(y=>y.TotalAmount);
            var TotalItem = db.SaleTransactions.Where(x => x.Customerid == mailID).Sum(y => y.Quantity);
            var CustomerName = db.Customers.Where(x=>x.CustomerEmail == mail).Select(y=> y.CustomerName + " " + y.CustomerSurname).FirstOrDefault();
            var CustomerCity = db.Customers.Where(x => x.CustomerEmail == mail).Select(y => y.CustomerCity).FirstOrDefault();
            ViewBag.City = CustomerCity;
            ViewBag.Name = CustomerName;
            ViewBag.Item = TotalItem;
            ViewBag.Sum = TotalSpent;
            ViewBag.SaleHistory = SaleHistory;
            ViewBag.MailID = mailID;
            ViewBag.Mail = mail;
            return View(value);

        }
        public ActionResult Orders()
        {
            var mail = GetCustomerEmail();
            var id = db.Customers.Where(x => x.CustomerEmail == mail.ToString()).Select(y => y.CustomerID).FirstOrDefault();
            var order = db.SaleTransactions.Where(x => x.Customerid == id).ToList();
            return View(order);
        }

        public ActionResult Message()
        {
            var mail = GetCustomerEmail();
            var messages = db.Messages.Where(x => x.Receiver == mail).OrderByDescending(y => y.MessageID).ToList();
            LoadMessageCounts(mail);
            return View(messages);
        }

        public ActionResult SentMessage()
        {
            var mail = GetCustomerEmail();
            var messages = db.Messages.Where(x => x.Sender == mail).OrderByDescending(y => y.MessageID).ToList();
            LoadMessageCounts(mail);
            return View(messages);
        }

        public ActionResult Compose(int id)
        {
            var messageID = db.Messages.Where(x => x.MessageID == id).ToList();
            var mail = GetCustomerEmail();
            var messages = db.Messages.Where(x => x.Sender == mail).ToList();
            LoadMessageCounts(mail);
            return View(messageID);
        }
        [HttpGet]
        public ActionResult NewCompose()
        {
            var mail = GetCustomerEmail();
            var messages = db.Messages.Where(x => x.Sender == mail).ToList();
            LoadMessageCounts(mail);
            return View();
        }
        [HttpPost]
        public ActionResult NewCompose(Message message)
        {
            var mail = GetCustomerEmail();
            message.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            message.Sender = mail;
            db.Messages.Add(message);
            db.SaveChanges();
            return View();
        }

        public ActionResult OrderTracking(string p)
        {
            var cargo = from x in db.Shipments select x;
            cargo = cargo.Where(y => y.TrackingCode.Contains(p));
            return View(cargo.ToList());
        }

        public ActionResult CustomerOrderTrackingDetail(string id)
        {
            var orderDetail = db.ShipmentTrackings.Where(x=>x.TrackingCode == id).ToList();
            return View(orderDetail);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        
        public PartialViewResult PartialSettings()
        {
            var mail = GetCustomerEmail();
            var CustomerID = db.Customers.Where(x=>x.CustomerEmail == mail).Select(y=>y.CustomerID).FirstOrDefault();
            var FindCustomer = db.Customers.Find(CustomerID);
            return PartialView("PartialSettings", FindCustomer);
        }

        public PartialViewResult Information()
        {
            var info = db.Messages.Where(x => x.Sender == "admin").ToList();
            return PartialView(info);
        }
        public ActionResult Settings(Customer c)
        {
            var customer = db.Customers.Find(c.CustomerID);
            customer.CustomerCity = c.CustomerCity;
            customer.CustomerPassword = c.CustomerPassword;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string GetCustomerEmail()
        {
            return Session["CustomerEmail"]?.ToString();
        }
        private void LoadMessageCounts(string mail)
        {
            ViewBag.inbox = db.Messages.Count(x => x.Receiver == mail).ToString();
            ViewBag.sent = db.Messages.Count(x => x.Sender == mail).ToString();
        }
    }
}