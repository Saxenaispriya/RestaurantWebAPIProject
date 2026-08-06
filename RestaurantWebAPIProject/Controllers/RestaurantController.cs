using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPIProject.BO.Interface;
using RestaurantWebAPIProject.Common.Models;
using RestaurantWebAPIProject.Common.Dtos;

namespace RestaurantWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("do_OrderRoute")]
        public IActionResult do_Order([FromBody] OrderRequestDto orderRequestPayload)
        {
            _restaurantService.Do_Orders(orderRequestPayload);
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

        [HttpDelete("deletetableRoute")]
        public IActionResult deleteTable([FromQuery] int _tablenumber)
        {
            _restaurantService.removeTable(_tablenumber);
            return Ok();
        }

        [HttpDelete("deleteFoodItemRoute")]
        public IActionResult deleteFoodItem([FromQuery] int _fooditem)
        {
            _restaurantService.removeFooditem(_fooditem);
            return Ok();
        }


        [HttpPost("completePaymentRoute")]
        public IActionResult CompletePayment(int tableNumber)
        {
            _restaurantService.CompletePayment(tableNumber);

            return Ok(new {message="Payment Completed Successfully"});
        }

    }
}
