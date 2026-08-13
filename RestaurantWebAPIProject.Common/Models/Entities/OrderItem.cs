using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int FoodItemId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; } = null;

        public FoodItem FoodItem { get; set; }
    }
}
