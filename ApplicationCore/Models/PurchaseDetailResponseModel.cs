using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseDetailResponseModel
    {
        public System.Guid PurchaseNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public DateTime PurchaseDateTime { get; set; }

        public string? PosterUrl { get; set; }
    }
}
