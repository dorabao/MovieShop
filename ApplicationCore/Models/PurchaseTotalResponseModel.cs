using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseTotalResponseModel
    {
        public int MovieId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
