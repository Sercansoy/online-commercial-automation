using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SalesController : Controller
    {

        // GET: Sales
        Context db = new Context();
        public ActionResult Index()
        {
            var value = db.SaleTransactions.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult NewSale()
        {
            GetDropDownList();
            return View();
        }
        [HttpPost]
        public ActionResult NewSale(SaleTransaction s)
        {

            s.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SaleTransactions.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FindSale(int id)
        {
            GetDropDownList();
            var value = db.SaleTransactions.Find(id);
            return View("FindSale", value);
        }
        [HttpPost]
        public ActionResult UpdateSale(SaleTransaction s)
        {
            var value = db.SaleTransactions.Find(s.SaleID);
            if (value == null)
            {
                return HttpNotFound("The sale transaction record to be updated could not be found.");
            }
            value.Customerid = s.Customerid;
            value.Staffid = s.Staffid;
            value.Quantity = s.Quantity;
            value.Price = s.Price;
            value.TotalAmount = s.TotalAmount;
            value.Date = s.Date;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SaleDetail(int id)
        {
            var sale = db.SaleTransactions.Where(x => x.SaleID == id).ToList();
            return View(sale);
        }

        private void GetDropDownList()
        {
            List<SelectListItem> ValueProduct = (from x in db.Products.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ProductName,
                                                     Value = x.ProductID.ToString()
                                                 }).ToList();
            List<SelectListItem> ValueCustomer = (from x in db.Customers.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CustomerName + " " + x.CustomerSurname,
                                                      Value = x.CustomerID.ToString()
                                                  }).ToList();
            List<SelectListItem> ValueStaff = (from x in db.Staffs.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.StaffName + " " + x.StaffSurname,
                                                   Value = x.StaffID.ToString()
                                               }).ToList();
            ViewBag.ValueProduct = ValueProduct;
            ViewBag.ValueCustomer = ValueCustomer;
            ViewBag.ValueStaff = ValueStaff;
        }
    }
}