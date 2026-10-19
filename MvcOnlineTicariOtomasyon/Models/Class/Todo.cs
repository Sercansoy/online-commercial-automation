using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(100)]
        public  string Title { get; set; }

        [Column(TypeName ="Bit")]
        public bool Status { get; set; }
    }
}