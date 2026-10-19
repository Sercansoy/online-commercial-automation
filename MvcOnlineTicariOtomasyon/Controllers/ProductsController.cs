using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        Context db = new Context();
        public ActionResult Index(int page=1)
        {
            var products = db.Products.Where(x => x.Status == true).ToList().ToPagedList(page, 10);
            return View(products);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> CategoryList = (from x in db.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.CategoryName,
                                                     Value = x.CategoryID.ToString()
                                                 }).ToList();

            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(int id)
        {
            var delete = db.Products.Find(id);
            delete.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FindProduct(int id)
        {
            List<SelectListItem> CategoryList = (from x in db.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.CategoryName,
                                                     Value = x.CategoryID.ToString()
                                                 }).ToList();

            ViewBag.CategoryList = CategoryList;

            var update = db.Products.Find(id);
            return View("UpdateProduct", update);
        }

        public ActionResult UpdateProduct(Product p)

        {

            var product = db.Products.Find(p.ProductID);
            product.ProductName = p.ProductName;
            product.Brand = p.Brand;
            product.Stock = p.Stock;
            product.CostPrice = p.CostPrice;
            product.SellingPrice = p.SellingPrice;
            product.Status = p.Status;
            product.Categoryid = p.Categoryid;
            product.ProductImage = p.ProductImage;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductList()
        {
            var value = db.Products.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult MakeSale(int id)
        {
            GetDropDownList();
            var valueID = db.Products.Find(id); 
            ViewBag.id = valueID.ProductID;
            ViewBag.Price = valueID.SellingPrice;
            return View();
        }
        [HttpPost]
        public ActionResult GetSale(SaleTransaction s)
        {
            s.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SaleTransactions.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index", "Sales");
        }
        private void GetDropDownList()
        {
            List<SelectListItem> ValueProduct = (from x in db.Products.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.ProductName,
                                                     Value = x.ProductID.ToString()
                                                 }).ToList();
            List<SelectListItem> ValueCustomer = (from x in db.Customers
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CustomerName + " " + x.CustomerSurname,
                                                      Value = x.CustomerID.ToString()
                                                  }).ToList();
            List<SelectListItem> ValueStaff = (from x in db.Staffs
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