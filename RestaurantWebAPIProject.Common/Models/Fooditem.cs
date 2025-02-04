using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models
{
    public class Fooditem
    {
        public int foodItemId { get; set; }
        public string foodItemName { get; set; }
        public int foodPrice { get; set; }
    }
}
