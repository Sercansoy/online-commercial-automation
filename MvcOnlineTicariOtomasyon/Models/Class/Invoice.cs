using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string InvoiceSerialNumber { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(6)]
        public string InvoiceSequenceNumber { get; set; }
        public DateTime Date { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(40)]
        public string TaxOffice { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(5)]
        public String Time { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30)]
        public string DeliveredBy { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30)]
        public string ReceivedBy { get; set; }
        public Decimal Total { get; set; }

        public ICollection<InvoiceItem>InvoiceItems { get; set; }
    }
}