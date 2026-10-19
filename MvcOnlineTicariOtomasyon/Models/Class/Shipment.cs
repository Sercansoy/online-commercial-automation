using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string TrackingCode { get; set; }   // Takip Kodu

        [Column(TypeName = "VarChar")]
        [StringLength(50)]
        public string Customer { get; set; }  // Alıcı

        [Column(TypeName = "VarChar")]
        [StringLength(50)]
        public string Staff { get; set; }      // İlgilenen Personel


        [Column(TypeName = "VarChar")]
        [StringLength(200)]
        public string Detail { get; set; }           // Not/Açıklama

        public DateTime Date { get; set; }         // Oluşturulma Tarihi
    }
}