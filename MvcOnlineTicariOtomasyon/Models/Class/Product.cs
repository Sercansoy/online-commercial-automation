using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30)]
        public string ProductName { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30)]
        public string Brand { get; set; }
        public short Stock { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool Status { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(250)]
        public string ProductImage { get; set; }

        public int Categoryid { get; set; }

        public virtual Category Category { get; set; }

        public  ICollection<SaleTransaction> SaleTransactions { get; set; }
    }
}