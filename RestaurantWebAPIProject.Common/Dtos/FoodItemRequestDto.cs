using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Dtos
{
    public class FoodItemRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Food Item Id should be greater than 0")]
        public int foodItemId { get; set; }

        [Required(ErrorMessage ="food name is required.")]
        public string foodName { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Food Price should be greater than 0.")]
        public int foodPrice { get; set; }
    }
}
