using RestaurantWebAPIProject.Common.Dtos;
using RestaurantWebAPIProject.Common.Models;
using RestaurantWebAPIProject.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPIProject.BO.Interface
{
    public interface IRestaurantService
    {
        //void removeTable(int _tablenumber);
       // void removeFooditem(int _fooditem);
        List<RestaurantTable> showAvailableTables();
        List<FoodItem> showMenuesItem();
        void Do_Orders(OrderRequestDto orderRequestPayload);
        int generateBill(int _tablenumber);
        void addTable(TableRequestDto tableRequestPayload);
        void addFoodItem(FoodItemRequestDto foodItemRequestPayload);

        void CompletePayment(int tableNumber);

        OrderResponseDto GetActiveOrder(int tableNumber);
    }
}
