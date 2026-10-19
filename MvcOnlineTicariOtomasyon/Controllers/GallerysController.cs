using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GallerysController : Controller
    {
        // GET: Gallerys
        Context db = new Context();
        public ActionResult Index()
        {
            var gallerys = db.Products.ToList(); 
            return View(gallerys);
        }
    }
}