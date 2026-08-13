using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models.Entities
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId {  get; set; }

        public string FoodItemName { get; set; }

        public int FoodPrice { get; set; }

        public List<OrderItem> OrderItems {  get; set; }
    }
}
