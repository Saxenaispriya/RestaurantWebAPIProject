using RestaurantWebAPIProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.DataAccess.Repository
{
    public class DataStorageRepository:IDataStorageRepository
    {
        private Dictionary<int, Fooditem> mydictfooditem = new Dictionary<int, Fooditem>();
        private Dictionary<int, Table> mydicttable = new Dictionary<int, Table>();
        public DataStorageRepository()
        {
            initMenu();
            initTables();
        }

        public Dictionary<int, Table> GetTableDictionary()
        {
            return mydicttable;
        }
       
        public Dictionary<int, Fooditem> GetFoodDictionary()
        {
            return mydictfooditem;
        }

        public Table getTableByTableNumber(int tableNumber)
        {
            return mydicttable[tableNumber];
        }

        public void AddTable(int tableNumber, Table table)
        {
            mydicttable.Add(tableNumber, table);
        }

        public void AddFood(int foodnumber, Fooditem food)
        {
            mydictfooditem.Add(foodnumber, food);
        }


        public Fooditem getFoodItemByTableNumber(int tableNumber)
        {
            return mydictfooditem[tableNumber];
        }

        public void StoreFoodItem(int tableNumber, Fooditem fooditem)
        {
            mydictfooditem.Add(tableNumber, fooditem);
        }
        public bool doesTableExist(int tablenumber)
        {
            return mydicttable.ContainsKey(tablenumber);
        }
        public bool doesFoodExist(int foodnumber)
        {
            return mydictfooditem.ContainsKey(foodnumber); 
   
        }
        public Fooditem getFoodItemByFoodItemNumber(int foodnumber)
        {
            return mydictfooditem[foodnumber];
        }
        public void removeFoodItemByFoodItemNumber(int foodnumber)
        {
            mydictfooditem.Remove(foodnumber);
        }
        public void removeTableByTableNumber(int tablenumber)
        {
            mydicttable.Remove(tablenumber);
        }

        public void initMenu()
        {
            var itemList = new List<string>(){
                 "Dal Fry", "Rice", "Kadhai Panner"
            };

            var foodPriceList = new List<int>(){
             100, 200, 400
            };

            for (int i = 0; i < itemList.Count; i++)
            {
                Fooditem foodItem = new Fooditem();
                foodItem.foodItemId = i+1;
                foodItem.foodItemName = itemList[i];
                foodItem.foodPrice = foodPriceList[i];
                mydictfooditem.Add(i, foodItem);
            }
        }
        public void initTables()
        {
            for (int i = 1; i <= 10; i++)
            {
                Table t = new Table();
                t.tableNumber = i;
                t.isTableOccupied = false;
                mydicttable.Add(i, t);
            }
        }


    }
}
