using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class StatisticsController : Controller
    {
        Context db = new Context();
        // GET: Statistics
        public ActionResult Index()
        {
            var customer = db.Customers.Count().ToString();
            ViewBag.Customer = customer;

            var product = db.Products.Count().ToString();
            ViewBag.Product = product;

            var staff = db.Staffs.Count().ToString();
            ViewBag.Staff = staff;

            var categorie = db.Categories.Count().ToString();
            ViewBag.Categorie = categorie;

            var stock = db.Products.Sum(x => x.Stock).ToString();
            ViewBag.Stock = stock;

            var brand = (from x in db.Products select x.Brand).Distinct().Count();
            ViewBag.Brand = brand;

            var stockAlert = db.Products.Count(x => x.Stock <= 20).ToString();
            ViewBag.StockAlert = stockAlert;

            var highestProduct = (from x in db.Products orderby x.SellingPrice descending select x.ProductName).FirstOrDefault();
            ViewBag.HighestProduct = highestProduct;

            var lowestProduct = (from x in db.Products orderby x.SellingPrice ascending select x.ProductName).FirstOrDefault();
            ViewBag.LowestProduct = lowestProduct;

            var refrigerator = db.Products.Count(x => x.ProductName == "Refrigerator").ToString();
            ViewBag.Refrigerator = refrigerator;

            var laptop = db.Products.Count(x => x.ProductName == "Laptop").ToString();
            ViewBag.Laptop = laptop;

            var revenue = db.SaleTransactions.Sum(x => (decimal?)x.TotalAmount) ?? 0;
            ViewBag.Revenue = revenue.ToString("N2") + " $";

            DateTime today = DateTime.Today;
            var todaysale = db.SaleTransactions.Count(x => x.Date == today).ToString();
            ViewBag.TodaySale = todaysale;

            var todayRevenue = db.SaleTransactions.Where(x => x.Date == today).Sum(x => (decimal?)x.TotalAmount) ?? 0;
            ViewBag.TodayRevenue = todayRevenue.ToString("N2") + " $";

            var topBrand = db.Products.GroupBy(x => x.Brand).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();
            ViewBag.TopBrand = topBrand;

            var bestSeller = db.Products
            .Where(x => x.ProductID == (db.SaleTransactions
            .GroupBy(y => y.Productid)
            .OrderByDescending(z => z.Sum(s => s.Quantity))
            .Select(f => f.Key)
            .FirstOrDefault()))
            .Select(g => g.ProductName)
            .FirstOrDefault();
            ViewBag.BestSeller = bestSeller ?? "N/A";

            return View();
        }

        public ActionResult SimpleTables()
        {
            var cityGroupList = from x in db.Customers
                                group x by x.CustomerCity into y
                                select new GroupCountViewModel
                                {
                                    City = y.Key,
                                    Count = y.Count()
                                };
            return View(cityGroupList.ToList());
        }

        public PartialViewResult DepartmentPartial()
        {
            var dep = from x in db.Staffs
                      group x by x.Department.DepartmentName into y
                      select new DepartmentViewModel
                      {
                          Department = y.Key,
                          count = y.Count()
                      };
            return PartialView(dep.ToList());
        }

        public PartialViewResult CustomersPartial()
        {
            var Customers = db.Customers.ToList();
            return PartialView(Customers);
        }

        public PartialViewResult ProductsPartial()
        {
            var products = db.Products.ToList();
            return PartialView(products);
        }

        
    }
}