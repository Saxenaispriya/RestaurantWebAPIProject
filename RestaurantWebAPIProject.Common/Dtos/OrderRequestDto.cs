using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantWebAPIProject.Common.Models;

namespace RestaurantWebAPIProject.Common.Dtos
{
    public class OrderRequestDto
    {
        public int tablenumber { get; set; }
        public List<OrderItemsRequestDto> orderslst { get; set; }
    }
}
