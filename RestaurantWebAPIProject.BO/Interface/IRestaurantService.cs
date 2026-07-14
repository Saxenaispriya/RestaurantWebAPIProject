using RestaurantWebAPIProject.Common.Dtos;
using RestaurantWebAPIProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.BO.Interface
{
    public interface IRestaurantService
    {
        void removeTable(int _tablenumber);
        void removeFooditem(int _fooditem);
        List<Table> showAvailableTables();
        List<Fooditem> showMenuesItem();
        void Do_Orders(OrderRequestDto orderRequestPayload);
        int generateBill(int _tablenumber);
        void addTable(TableRequestDto tableRequestPayload);
        void addFoodItem(FoodItemRequestDto foodItemRequestPayload);
    }
}
