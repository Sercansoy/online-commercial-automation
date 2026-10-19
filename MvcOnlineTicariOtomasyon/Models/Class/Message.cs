using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        [Column(TypeName = "NVarChar")]
        [StringLength(50)]
        public string Sender { get; set; }

        [Column(TypeName = "NVarChar")]
        [StringLength(50)]
        public string Receiver { get; set; }

        [Column(TypeName = "NVarChar")]
        [StringLength(50)]
        public string Subject { get; set; }

        [Column(TypeName = "NVarChar")]
        [StringLength(2000)]
        public string Content { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
    }
}