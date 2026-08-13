using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int RestaurantTableId { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public RestaurantTable RestaurantTable { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
