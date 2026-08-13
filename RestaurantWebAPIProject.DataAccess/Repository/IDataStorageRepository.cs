using RestaurantWebAPIProject.Common.Models;
using RestaurantWebAPIProject.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.DataAccess.Repository
{
    public interface IDataStorageRepository
    {
        List<RestaurantTable>GetTable();
        RestaurantTable getTableByTableNumber(int tableNumber);

        void AddTable(RestaurantTable table);
        void AddFood(FoodItem food);
        bool doesTableExist(int tablenumber);
        bool doesFoodExist(string foodName);
        List<FoodItem> GetFood();
        Common.Models.Entities.Order? GetActiveOrder(int tableId);
        void AddOrder(Common.Models.Entities.Order order);
        void SaveChanges();
    }
}
