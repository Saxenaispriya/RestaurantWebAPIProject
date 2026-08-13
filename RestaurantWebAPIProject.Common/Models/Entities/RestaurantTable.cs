using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.Common.Models.Entities
{
    public class RestaurantTable
    {
        [Key]
        public int TableId { get; set; }
        public int TableNumber {  get; set; }

        public bool IsTableOccupied {  get; set; } = false;

        public List<Order> Orders { get; set; } = new();
    }
}
