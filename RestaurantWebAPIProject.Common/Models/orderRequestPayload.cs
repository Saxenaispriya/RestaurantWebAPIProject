using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models
{
    public class orderRequestPayload
    {
        public int tablenumber { get; set; }
        public List<Order> orderslst { get; set; }
    }
}
