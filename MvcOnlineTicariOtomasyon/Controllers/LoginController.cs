using MvcOnlineTicariOtomasyon.Models.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Register(Customer c)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(c);
            }

            try
            {
                db.Customers.Add(c);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                    foreach (var error in validationErrors.ValidationErrors)
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return PartialView(c);
            }

            return PartialView();
        }
        [HttpGet]
        public ActionResult CustomerLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerLogin(Customer c)
        {
            var value = db.Customers.FirstOrDefault(x => x.CustomerEmail == c.CustomerEmail && x.CustomerPassword == c.CustomerPassword);
            if (value != null)
            {
                FormsAuthentication.SetAuthCookie(value.CustomerEmail, false);
                Session["CustomerEmail"] = value.CustomerEmail.ToString();
                return RedirectToAction("Index", "CustomerPanel");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var login = db.Admins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if (login != null)
            {
                FormsAuthentication.SetAuthCookie(login.UserName, false);
                Session["UserName"] = login.UserName.ToString();
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Categories") });
            }
            else
            {
                return Json(new { success = false, message = "Incorrect username or password. Please try again." });
            }
            
        }
    }
}