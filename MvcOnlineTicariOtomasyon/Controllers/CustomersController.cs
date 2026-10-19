using MvcOnlineTicariOtomasyon.Models.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        Context db = new Context();
        public ActionResult Index()
        {
            var customers = db.Customers.Where(x => x.Status == true).ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer c)
        {
            c.Status = true;
            db.Customers.Add(c);
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult DeleteCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            customer.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FindCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            return View("FindCustomer", customer);
        }

        public ActionResult UpdateCustomer(Customer c)
        {
            if (!ModelState.IsValid)
            {
                return View("FindCustomer");
            }
            var Customer = db.Customers.Find(c.CustomerID);
            Customer.CustomerName = c.CustomerName;
            Customer.CustomerSurname = c.CustomerSurname;
            Customer.CustomerEmail = c.CustomerEmail;
            Customer.CustomerCity = c.CustomerCity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CustomerHistory(int id)
        {
            var customer = db.SaleTransactions.Where(x => x.Customerid == id).ToList();
            var customerName = db.Customers.Where(x => x.CustomerID == id).Select(y => y.CustomerName + " " + y.CustomerSurname).FirstOrDefault();
            ViewBag.CustomerName = customerName;
            return View(customer);
        }
    }
}