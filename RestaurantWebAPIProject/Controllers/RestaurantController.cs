using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPIProject.BO.Interface;
using RestaurantWebAPIProject.Common.Models;
using RestaurantWebAPIProject.Common.Dtos;
using Azure.Messaging.ServiceBus;
using RestaurantWebAPIProject.Messaging;
using System.Text.Json;

namespace RestaurantWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ServiceBusMessageSender _messageSender;
        public RestaurantController(IRestaurantService restaurantService,ServiceBusMessageSender messageSender)
        {
            _restaurantService = restaurantService;
            _messageSender = messageSender;
        }

        [HttpPost("do_OrderRoute")]
        public async Task<IActionResult> do_Order([FromBody] OrderRequestDto orderRequestPayload)
        {
           int orderId= _restaurantService.Do_Orders(orderRequestPayload);

            var orderCreatedMessage = new OrderCreatedMessage
            {
                OrderId = orderId,
                TableNumber = orderRequestPayload.tablenumber,
                Status = "OrderCreated"
            };

            var message=JsonSerializer.Serialize(orderCreatedMessage);  

            await _messageSender.SendMessageAsync(message);
            return Ok();
        }

        [HttpPost("addTableRoute")]
        public IActionResult addTable([FromBody] TableRequestDto tableRequestPayload)
        {
            _restaurantService.addTable(tableRequestPayload);
            return Ok();
        }

        [HttpPost("addFoodItemRoute")]
        public IActionResult addFoodItem([FromBody] FoodItemRequestDto foodItemRequestPayload)
        {
           _restaurantService.addFoodItem(foodItemRequestPayload);
           return Ok();
        }

        [HttpGet("AvailabletableRoute")]
        public IActionResult getAvailableTables()
        {
            var res = _restaurantService.showAvailableTables();
            return Ok(res);
        }

        [HttpGet("MenuesRoute")]
        public IActionResult getMenues()
        {
            var response = _restaurantService.showMenuesItem();
            return Ok(response);
        }

        [HttpGet("generatebillRoute")]
        public IActionResult get_GenerateBill(int tablenumber)
        {
            int payment;
            payment = _restaurantService.generateBill(tablenumber);
            if (payment == 0)
            {
                return BadRequest();
            }
            return Ok(payment);
        }

        //[HttpDelete("deletetableRoute")]
        //public IActionResult deleteTable([FromQuery] int _tablenumber)
        //{
        //    _restaurantService.removeTable(_tablenumber);
        //    return Ok();
        //}

        //[HttpDelete("deleteFoodItemRoute")]
        //public IActionResult deleteFoodItem([FromQuery] int _fooditem)
        //{
        //    _restaurantService.removeFooditem(_fooditem);
        //    return Ok();
        //}

        [HttpGet("getOrderRoute")]
        public IActionResult GetOrder(int tableNumber)
        {
            var order = _restaurantService.GetActiveOrder(tableNumber);

            return Ok(order);
        }


        [HttpPost("completePaymentRoute")]
        public IActionResult CompletePayment(int tableNumber)
        {
            _restaurantService.CompletePayment(tableNumber);

            return Ok(new { message = "Payment Completed Successfully" });
        }

    }
}
