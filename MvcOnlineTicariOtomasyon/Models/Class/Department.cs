using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(40)]
        public string DepartmentName { get; set; }

        public bool Status { get; set; }

        public ICollection<Staff> Staffs { get; set; }
    }
}