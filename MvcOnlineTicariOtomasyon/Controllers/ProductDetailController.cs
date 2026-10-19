using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        Context db = new Context();
        public ActionResult Index()
        {
            ProductListDto dto = new ProductListDto();
            dto.products= db.Products.Where(x=>x.ProductID==1).ToList();
            dto.ProductDescriptions = db.Details.Where(y=>y.DetailID==1).ToList();
            return View(dto);
        }
    }
}