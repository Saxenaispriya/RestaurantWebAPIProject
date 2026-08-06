using RestaurantWebAPIProject.BO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantWebAPIProject.Common.Models;
using System.Text.Json;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using RestaurantWebAPIProject.DataAccess.Repository;
using RestaurantWebAPIProject.Common.Dtos;
using RestaurantWebAPIProject.Common.Exceptions;
using System.Collections;


namespace RestaurantWebAPIProject.BO.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataStorageRepository _IdataStorageRepository;

        public RestaurantService(IDataStorageRepository dataStorageService)
        {
            _IdataStorageRepository = dataStorageService;
        }

        public void Do_Orders(OrderRequestDto orderRequestPayload)
        {
            if (!_IdataStorageRepository.doesTableExist(orderRequestPayload.tablenumber))
            {
                throw new NotFoundException("Table does not exist");

               
            }

            Table t = _IdataStorageRepository.getTableByTableNumber(orderRequestPayload.tablenumber);


            if(orderRequestPayload.orderslst==null || orderRequestPayload.orderslst.Count==0) 
            {
                throw new BadRequestException("Please add at least one food item");
            }

            int orderId;

            if(t.orderlist==null || t.orderlist.Count==0)
            {
                orderId = _IdataStorageRepository.GetNextOrderId();
            }
            else
            {
                orderId = t.orderlist[0].orderId;
            }

            foreach(OrderItemsRequestDto orderIncomingOrder in orderRequestPayload.orderslst)
            {
                Order? existingOrderItem = t.orderlist.FirstOrDefault(item => item._fooditemNumber == orderIncomingOrder._foodItemNumber);

                if(existingOrderItem!=null)
                {
                    existingOrderItem._quantity = existingOrderItem._quantity + orderIncomingOrder._quantity;
                }
                else
                {
                    Order newOrderItem = new Order
                    {
                        orderId = orderId, _fooditemNumber = orderIncomingOrder._foodItemNumber, _quantity = orderIncomingOrder._quantity
                    };

                    t.orderlist.Add(newOrderItem);
                }
                   
            }
            t.isTableOccupied = true;

        }

        public int generateBill(int _tablenumber)
        {
            int sum = 0;

            if (!_IdataStorageRepository.doesTableExist(_tablenumber))
            {
                throw new NotFoundException("Table does not exist");
            }
                Table t = _IdataStorageRepository.getTableByTableNumber(_tablenumber);

                if(t.orderlist==null || t.orderlist.Count==0)
                {
                    throw new BadRequestException("No order found for this table");
                }

                foreach (Order or in t.orderlist)
                {
                    if (!_IdataStorageRepository.doesFoodExist(or._fooditemNumber))
                    {
                        throw new BadRequestException("Food Item not found");
                    }

                    Fooditem fditm = _IdataStorageRepository.getFoodItemByFoodItemNumber(or._fooditemNumber);
                    sum += fditm.foodPrice * or._quantity;
                }
            
            return sum;
        }

        public void removeFooditem(int _fooditem)
        {
            if (_IdataStorageRepository.doesFoodExist(_fooditem))
            {
                _IdataStorageRepository.removeFoodItemByFoodItemNumber(_fooditem);
            }
        }

        public void removeTable(int _tablenumber)
        {
            if (_IdataStorageRepository.doesTableExist(_tablenumber))
            {
                _IdataStorageRepository.removeTableByTableNumber(_tablenumber);
            }
        }

        public List<Table> showAvailableTables()
        {
            tableResponse td = new tableResponse();
            foreach (KeyValuePair<int, Table> t in _IdataStorageRepository.GetTableDictionary())
            {
                td.tablelist.Add(t.Value);
            }
            return td.tablelist;
        }

        public List<Fooditem> showMenuesItem()
        {
            foodItemResponse fd = new foodItemResponse();
            foreach (KeyValuePair<int, Fooditem> fditem in _IdataStorageRepository.GetFoodDictionary())
            {
                fd.fooditemlist.Add(fditem.Value);
            }
            return fd.fooditemlist;
        }

        public void addTable(TableRequestDto tableRequestPayload)
        {
            if(_IdataStorageRepository.doesTableExist(tableRequestPayload.tableNumber))
            {
                throw new BadRequestException("Table Id is already exist");
            }

            Table t = new Table();
            t.tableNumber = tableRequestPayload.tableNumber;
            t.isTableOccupied = false;
            _IdataStorageRepository.AddTable(tableRequestPayload.tableNumber,t);
        }

        public void addFoodItem(FoodItemRequestDto foodItemRequestPayload)
        {
            if(_IdataStorageRepository.doesFoodExist(foodItemRequestPayload.foodItemId))
            {
                throw new BadRequestException("Food Item Id already exist");
            }

            Fooditem fd=new Fooditem();
            fd.foodItemId = foodItemRequestPayload.foodItemId;
            fd.foodItemName=foodItemRequestPayload.foodName;
            fd.foodPrice= foodItemRequestPayload.foodPrice;

            _IdataStorageRepository.AddFood(foodItemRequestPayload.foodItemId,fd);
        }

        public void CompletePayment(int tableNumber)
        {
            if(!_IdataStorageRepository.doesTableExist(tableNumber))
            {
                throw new NotFoundException("Table does not exist");
            }

            Table table = _IdataStorageRepository.getTableByTableNumber(tableNumber);

            if(table.orderlist==null || table.orderlist.Count==0)
            {
                throw new BadRequestException("No Active Order found for this table");
            }

            table.orderlist.Clear();
            table.isTableOccupied = false;
        }
    }
}
