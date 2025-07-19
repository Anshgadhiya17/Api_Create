using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepositry OrderRepositry;

        public OrderController(OrderRepositry _OrderRepositry)
        {
            OrderRepositry = _OrderRepositry;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var order = OrderRepositry.OrderDetail();
            return Ok(order);
        }

        [HttpGet]
        [Route("OrderDropDown")]
        public IActionResult OrderDropDownList()
        {
            var order = OrderRepositry.OrderDropDown();
            return Ok(order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var Order = OrderRepositry.OrderById(id);
            if (Order == null)
            {
                return NotFound(new { Message = $"Order with ID {id} not found." });
            }
            return Ok(Order);
        }

        [HttpPost]
        public IActionResult InsertOrder([FromBody] OrderModel Order)
        {
            var validator = new OrderValidator();
            var results = validator.Validate(Order);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Order == null)
            {
                return BadRequest();
            }

            bool isInserted = OrderRepositry.InsertOrder(Order);

            if (isInserted)
            {
                return Ok(new { Message = "Order Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderModel Order)
        {
            var validator = new OrderValidator();
            var results = validator.Validate(Order);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Order == null || id != Order.OrderID)
            {
                return BadRequest();
            }

            var isUpdated = OrderRepositry.UpdateOrder(Order);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid Order ID provided." });
            }

            bool isDeleted = OrderRepositry.DeleteOrder(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "Order deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the Order." });
        }
    }
}
