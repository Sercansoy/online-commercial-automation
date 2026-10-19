using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30, ErrorMessage = "Please enter a name between 2 and 30 characters.")]
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(30, ErrorMessage = "Customer Surname cannot exceed 30 characters.")]
        [Required(ErrorMessage = "Customer Surname is required.")]
        public string CustomerSurname { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string CustomerEmail { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(15, ErrorMessage = "City name is too long.")]
        [Required(ErrorMessage = "City information is required.")]
        public string CustomerCity { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(20)]
        public string CustomerPassword {  get; set; }

        public bool Status { get; set; }

        public ICollection<SaleTransaction> SaleTransactions { get; set; }
    }
}