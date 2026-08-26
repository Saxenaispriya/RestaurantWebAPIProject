using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessorFunction.Models
{
    internal class OrderCreatedMessage
    {
        public int OrderId { get; set; }
        public int TableNumber { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
