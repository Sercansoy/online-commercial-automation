using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Class
{
    public class ShipmentTracking
    {
        [Key]
        public int ShipmentTrackingId { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string TrackingCode { get; set; }  // Hangi kargoya ait olduğu

        [Column(TypeName = "VarChar")]
        [StringLength(100)]
        public string StatusDetail { get; set; }  // Örn: "Kargo dağıtıma çıktı"

        public DateTime Date { get; set; }        
    }
}