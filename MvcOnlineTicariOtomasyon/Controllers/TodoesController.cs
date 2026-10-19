using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class TodoesController : Controller
    {
        // GET: Todoes
        Context db = new Context();
        public ActionResult Index()
        {
            var customer = db.Customers.Count().ToString();
            ViewBag.Customer = customer;

            var product = db.Products.Count().ToString();
            ViewBag.Product = product;

            var categories = db.Categories.Count().ToString();
            ViewBag.Categories = categories;

            int city = db.Customers.Select(x => x.CustomerCity).Distinct().Count();
            ViewBag.City = city;

            var todo = db.Todoes.ToList();
            return View(todo);
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id, int status)
        {
            
                var todo = db.Todoes.Find(id);
                todo.Status = status == 1;
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }
    }
}