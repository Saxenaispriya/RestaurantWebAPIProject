using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models
{
    public class foodItemRequestPayload
    {
        public int foodItemId {  get; set; }
        public string foodName { get; set; }
        public int foodPrice { get; set; }
    }
}
