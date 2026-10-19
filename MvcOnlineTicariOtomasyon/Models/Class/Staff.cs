using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30)]
        public string StaffName { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30)]
        public string StaffSurname { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(250)]
        public string StaffImage { get; set; }

        public ICollection<SaleTransaction> SaleTransactions { get; set; }

        public int Departmentid { get; set; }

        public virtual Department Department { get; set; }
    }
}