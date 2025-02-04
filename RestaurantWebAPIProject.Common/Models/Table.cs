using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models
{
    public class Table
    {
        public Table()
        {
            orderlist = new List<Order>();
        }
        public Table(int _tableNumber)
        {
            tableNumber = _tableNumber;
            orderlist = new List<Order>();
        }
        public int tableNumber { get; set; }
        public bool isTableOccupied { get; set; } = false;

        //[JsonProperty("orderlist")] // Ensure this is properly serialized
        public List<Order> orderlist { get; set; } // Changed from field to property
    }
}
