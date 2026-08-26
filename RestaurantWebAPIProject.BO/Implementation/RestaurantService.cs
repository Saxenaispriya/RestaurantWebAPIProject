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
using RestaurantWebAPIProject.Common.Models.Entities;


namespace RestaurantWebAPIProject.BO.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataStorageRepository _IdataStorageRepository;

        public RestaurantService(IDataStorageRepository dataStorageService)
        {
            _IdataStorageRepository = dataStorageService;
        }

        public int Do_Orders(OrderRequestDto orderRequestPayload)
        {
            RestaurantTable table = _IdataStorageRepository.getTableByTableNumber(orderRequestPayload.tablenumber);

            if(table==null)
            {
                throw new NotFoundException("Table does not exist");
            }

            if (orderRequestPayload.orderslst == null || orderRequestPayload.orderslst.Count == 0)
            {
                throw new BadRequestException("Please add at least one food item");
            }

            Common.Models.Entities.Order? order = _IdataStorageRepository.GetActiveOrder(table.TableId);

            if (order == null)
            {
                order = new Common.Models.Entities.Order()
                {
                    RestaurantTableId= table.TableId,
                    Status="Active"
                };
                _IdataStorageRepository.AddOrder(order);
            }

            foreach (OrderItemsRequestDto orderIncomingOrder in orderRequestPayload.orderslst)
            {
                OrderItem? existingItem = order.OrderItems.FirstOrDefault(item => item.FoodItemId == orderIncomingOrder._foodItemNumber);

                if (existingItem != null)
                {
                    existingItem.Quantity += orderIncomingOrder._quantity;
                }
                else
                {
                    OrderItem newItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        FoodItemId = orderIncomingOrder._foodItemNumber,
                        Quantity = orderIncomingOrder._quantity
                    };

                    order.OrderItems.Add(newItem);
                }
            }

            table.IsTableOccupied = true;

            _IdataStorageRepository.SaveChanges();

            return order.OrderId;
        }

        public int generateBill(int _tablenumber)
        {
            RestaurantTable table = _IdataStorageRepository.getTableByTableNumber(_tablenumber);

            if(table==null)
            {
                throw new NotFoundException("Table does not exist");
            }

            Common.Models.Entities.Order order = _IdataStorageRepository.GetActiveOrder(table.TableId);

            if(order==null)
            {
                throw new BadRequestException("No active order for this table");
            }

            int totalAmount = 0;

            foreach (OrderItem item in order.OrderItems)
            {
                totalAmount += item.Quantity * item.FoodItem.FoodPrice;
            }

            return totalAmount;
        }

        //public void removeFooditem(int _fooditem)
        //{
        //    if (_IdataStorageRepository.doesFoodExist(_fooditem))
        //    {
        //        _IdataStorageRepository.removeFoodItemByFoodItemNumber(_fooditem);
        //    }
        //}

        //public void removeTable(int _tablenumber)
        //{
        //    if (_IdataStorageRepository.doesTableExist(_tablenumber))
        //    {
        //        _IdataStorageRepository.removeTableByTableNumber(_tablenumber);
        //    }
        //}

        public List<RestaurantTable> showAvailableTables()
        {
            return _IdataStorageRepository.GetTable();
        }

        public List<FoodItem> showMenuesItem()
        {
            return _IdataStorageRepository.GetFood();
        }

        public void addTable(TableRequestDto tableRequestPayload)
        {
            if(_IdataStorageRepository.doesTableExist(tableRequestPayload.tableNumber))
            {
                throw new BadRequestException("Table Id is already exist");
            }

            RestaurantTable table = new RestaurantTable()
            {
                TableNumber = tableRequestPayload.tableNumber,
                IsTableOccupied = false
            };
           
            _IdataStorageRepository.AddTable(table);
        }

        public void addFoodItem(FoodItemRequestDto foodItemRequestPayload)
        {
            if(_IdataStorageRepository.doesFoodExist(foodItemRequestPayload.foodName))
            {
                throw new BadRequestException("Food Name already exist");
            }

            FoodItem fd = new FoodItem
            {
                FoodItemName = foodItemRequestPayload.foodName,
                FoodPrice = foodItemRequestPayload.foodPrice
            };

            _IdataStorageRepository.AddFood(fd);
        }

        public void CompletePayment(int tableNumber)
        {
            RestaurantTable table = _IdataStorageRepository.getTableByTableNumber(tableNumber);

            if(table==null)
            {
                throw new NotFoundException("Table does not exist");
            }

            Common.Models.Entities.Order? order = _IdataStorageRepository.GetActiveOrder(table.TableId);

            if (order == null)
            {
                throw new BadRequestException(
                    "No active order found for this table");
            }

            order.Status = "Completed";
            order.CompletedAt = DateTime.UtcNow;

            table.IsTableOccupied = false;

            _IdataStorageRepository.SaveChanges();
        }

        public OrderResponseDto GetActiveOrder(int tableNumber)
        {
            RestaurantTable? table =
                _IdataStorageRepository.getTableByTableNumber(tableNumber);

            if (table == null)
            {
                throw new NotFoundException("Table does not exist");
            }

            Common.Models.Entities.Order? order =
                _IdataStorageRepository.GetActiveOrder(table.TableId);

            if (order == null)
            {
                throw new NotFoundException("No active order found for this table");
            }

            OrderResponseDto response = new OrderResponseDto
            {
                OrderId = order.OrderId,
                TableNumber = table.TableNumber,
                Status = order.Status
            };

            foreach (OrderItem item in order.OrderItems)
            {
                response.Items.Add(new OrderItemResponseDto
                {
                    FoodItemId = item.FoodItemId,
                    FoodItemName = item.FoodItem.FoodItemName,
                    FoodPrice = item.FoodItem.FoodPrice,
                    Quantity = item.Quantity
                });
            }

            return response;
        }
    }
}
