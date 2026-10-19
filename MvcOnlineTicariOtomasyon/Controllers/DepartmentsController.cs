using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class DepartmentsController : Controller
    {
        // GET: Departments
        Context db = new Context();
        public ActionResult Index()
        {
            var Department = db.Departments.Where(x => x.Status == true).ToList();
            return View(Department);
        }
        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddDepartment(Department d)
        {
            d.Status = true;
            db.Departments.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveDepartment(int id)
        {
            var department = db.Departments.Find(id);
            department.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FindDepartment(int id)
        {
            var department = db.Departments.Find(id);
            return View("FindDepartment", department);
        }

        public ActionResult UpdateDepartment(Department d)
        {
            var department = db.Departments.Find(d.DepartmentID);
            department.DepartmentName = d.DepartmentName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailDepartment(int id)
        {
            var dep = db.Staffs.Where(x => x.Departmentid == id).ToList();
            var depName = db.Departments.Where(x => x.DepartmentID == id).Select(i => i.DepartmentName).FirstOrDefault();
            ViewBag.DepartmentName = depName;
            return View(dep);
        }

        public ActionResult StaffSales(int id)
        {
            var staff = db.SaleTransactions.Where(x=>x.Staffid == id).ToList();
            var Vstaff= db.Staffs.Where(x=>x.StaffID == id).Select(y => y.StaffName + " " + y.StaffSurname).FirstOrDefault();
            ViewBag.dstaff = Vstaff;
            return View(staff);
        }
    }
}