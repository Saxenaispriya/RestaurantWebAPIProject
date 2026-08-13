using Microsoft.EntityFrameworkCore;
using RestaurantWebAPIProject.Common.Models;
using RestaurantWebAPIProject.Common.Models.Entities;
using RestaurantWebAPIProject.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.DataAccess.Repository
{
    public class DataStorageRepository:IDataStorageRepository
    {
        private RestaurantDbContext _context;
        private int _lastOrderId = 0;

        public DataStorageRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public List<RestaurantTable> GetTable()
        {
            return _context.RestaurantTables.ToList();  
        }

        public List<FoodItem> GetFood()
        {
            return _context.FoodItems.ToList();
        }

        public RestaurantTable getTableByTableNumber(int tableNumber)
        {
            return _context.RestaurantTables.FirstOrDefault(t => t.TableNumber == tableNumber);
        }

        public void AddTable(RestaurantTable table)
        {
            _context.RestaurantTables.Add(table);
            _context.SaveChanges();
        }

        public void AddFood(FoodItem food)
        {
            _context.FoodItems.Add(food);
            _context.SaveChanges();
        }

        public bool doesTableExist(int tablenumber)
        {
            return _context.RestaurantTables.Any(t => t.TableNumber == tablenumber);
        }
        public bool doesFoodExist(string foodName)
        {
            return _context.FoodItems.Any(f => f.FoodItemName == foodName);
        }

        public void AddOrder(Common.Models.Entities.Order order)
        {
            _context.Orders.Add(order);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Common.Models.Entities.Order? GetActiveOrder(int tableId)
        {
           return _context.Orders.Include(o=>o.OrderItems)
                .ThenInclude(oi=>oi.FoodItem)
                .FirstOrDefault(o=>o.RestaurantTableId == tableId && o.Status=="Active");
        }
    }
}
