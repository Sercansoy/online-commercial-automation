using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Class;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class StaffsController : Controller
    {
        Context db = new Context();
        // GET: Staffs
        public ActionResult Index()
        {
            var staffList = db.Staffs.ToList();
            return View(staffList);
        }

        [HttpGet]
        public ActionResult AddStaff()
        {
            List<SelectListItem> DepartmentList = (from x in db.Departments.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.DepartmentName,
                                                       Value = x.DepartmentID.ToString()
                                                   }).ToList();

            ViewBag.DepartmentList = DepartmentList;
            return View();
        }

        [HttpPost]
        public ActionResult AddStaff(Staff s)
        {
            if (Request.Files.Count > 0 && !string.IsNullOrEmpty(Request.Files[0].FileName))
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Image/" + fileName + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                s.StaffImage = "/Image/" + fileName + extension;
            }
            else if (!string.IsNullOrEmpty(s.StaffImage))
            {

            }
            db.Staffs.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FindStaff(int id)
        {
            List<SelectListItem> DepartmentList = (from x in db.Departments.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.DepartmentName,
                                                       Value = x.DepartmentID.ToString()
                                                   }).ToList();

            ViewBag.DepartmentList = DepartmentList;
            var staff = db.Staffs.Find(id);
            return View("FindStaff", staff);
        }

        public ActionResult UpdateStaff(Staff s)
        {
            var staff = db.Staffs.Find(s.StaffID);
            staff.StaffName = s.StaffName;
            staff.StaffSurname = s.StaffSurname;
            staff.Departmentid = s.Departmentid;

            if (Request.Files.Count > 0 && !string.IsNullOrEmpty(Request.Files[0].FileName))
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Image/" + fileName + extension;

                Request.Files[0].SaveAs(Server.MapPath(path));

                staff.StaffImage = "/Image/" + fileName + extension;
            }
            else if (!string.IsNullOrEmpty(s.StaffImage))
            {
                staff.StaffImage = s.StaffImage;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DetailStaffList()
        {
            var staff = db.Staffs.ToList();
            return View(staff);
        }
    }
}