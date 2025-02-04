using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models
{
    public class foodItemResponse
    {
        public List<Fooditem> fooditemlist { get; set; } = new List<Fooditem>();
    }
}
