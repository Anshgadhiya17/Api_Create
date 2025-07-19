using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailRepositry OrderDetailRepositry;

        public OrderDetailController(OrderDetailRepositry _OrderDetailRepositry)
        {
            OrderDetailRepositry = _OrderDetailRepositry;
        }

        [HttpGet]
        public IActionResult GetAllOrderDetail()
        {
            var orderDetail = OrderDetailRepositry.OrderDetail();
            return Ok(orderDetail);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetailById(int id)
        {
            var OrderDetail = OrderDetailRepositry.OrderDetailById(id);
            if (OrderDetail == null)
            {
                return NotFound(new { Message = $"OrderDetail with ID {id} not found." });
            }
            return Ok(OrderDetail);
        }

        [HttpPost]
        public IActionResult InsertOrderDetail([FromBody] OrderDetailModel OrderDetail)
        {
            var validator = new OrderDetailValidator();
            var results = validator.Validate(OrderDetail);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (OrderDetail == null)
            {
                return BadRequest();
            }

            bool isInserted = OrderDetailRepositry.InsertOrderDetail(OrderDetail);

            if (isInserted)
            {
                return Ok(new { Message = "OrderDetail Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id, [FromBody] OrderDetailModel OrderDetail)
        {
            var validator = new OrderDetailValidator();
            var results = validator.Validate(OrderDetail);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (OrderDetail == null || id != OrderDetail.OrderDetailID)
            {
                return BadRequest();
            }

            var isUpdated = OrderDetailRepositry.UpdateOrderDetail(OrderDetail);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid OrderDetail ID provided." });
            }

            bool isDeleted = OrderDetailRepositry.DeleteOrderDetail(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "OrderDetail deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the OrderDetail." });
        }
    }
}
