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
    public class CategoriesController : Controller
    {
        // GET: Categories
        Context db = new Context();
        public ActionResult Index(int page=1)
        {
            var categories = db.Categories.ToList().ToPagedList(page,10);
            return View(categories);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {  
            return View();         
        }
        [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveCategory(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FindCategory(int id)
        {
            var category = db.Categories.Find(id);
            return View("UpdateCategory", category);
        }

        public ActionResult UpdateCategory(Category c)
        {
            var category = db.Categories.Find(c.CategoryID);
            category.CategoryName = c.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            List list = new List();
            list.Categorys = new SelectList(db.Categories, "CategoryID", "CategoryName");
            list.Products = new SelectList(db.Products, "ProductID", "ProductName");
            return View(list);
        }

        public JsonResult ProductList(int p)
        {
            var list =(from x in db.Products
                       join y in db.Categories
                       on x.Category.CategoryID equals y.CategoryID
                       where x.Category.CategoryID == p
                       select new
                       {
                           Text = x.ProductName,
                           Value = x.ProductID.ToString(),
                       }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}