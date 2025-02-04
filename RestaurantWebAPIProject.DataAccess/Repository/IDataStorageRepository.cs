using RestaurantWebAPIProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.DataAccess.Repository
{
    public interface IDataStorageRepository
    {
        Dictionary<int, Table> GetTableDictionary();

        Dictionary<int, Fooditem> GetFoodDictionary();
        Table getTableByTableNumber(int tableNumber);

        void AddTable(int tableNumber, Table table);
        void AddFood(int foodnumber, Fooditem food);
        Fooditem getFoodItemByTableNumber(int tableNumber);

        void StoreFoodItem(int tableNumber, Fooditem fooditem);
        bool doesTableExist(int tablenumber);
        bool doesFoodExist(int foodnumber);
        Fooditem getFoodItemByFoodItemNumber(int foodnumber);
        void removeFoodItemByFoodItemNumber(int foodnumber);
        void removeTableByTableNumber(int tablenumber);
        void initMenu();
        void initTables();
    }
}
