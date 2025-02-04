using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models
{
    public class Order
    {
        public Order() { }

        public Order(int fooditemNumber, int quanty)
        {
            this._fooditemNumber = fooditemNumber;
            this._quantity = quanty;
        }
        public int orderId { get; set; }
       
        public int _fooditemNumber { get; set; }
        public int _quantity { get; set; }
    }
}
