using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class InvoiceTg
    {
        public IEnumerable <Invoice> Invoices { get; set; }
        public IEnumerable <InvoiceItem> InvoiceItems { get; set; }
    }
}