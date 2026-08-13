using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int TableNumber { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }

    public class OrderItemResponseDto
    {
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public int FoodPrice { get; set; }
        public int Quantity { get; set; }
    }
}
