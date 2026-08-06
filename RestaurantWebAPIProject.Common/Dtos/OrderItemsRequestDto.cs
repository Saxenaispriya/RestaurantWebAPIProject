using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Dtos
{
    public class OrderItemsRequestDto
    {
        [Range(1, int.MaxValue,
        ErrorMessage = "Food item number should be greater than 0")]
        public int _foodItemNumber { get; set; }

        [Range(1, int.MaxValue,
        ErrorMessage = "Quantity should be greater than 0")]
        public int _quantity { get; set; }
    }
}
