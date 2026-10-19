using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Detail
    {
        [Key]
        public int DetailID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        public string productName { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(2000)]
        public string productDetail { get; set; }
    }
}