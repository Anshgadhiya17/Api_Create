using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepositry CustomerRepositry;

        public CustomerController(CustomerRepositry _CustomerRepositry)
        {
            CustomerRepositry = _CustomerRepositry;
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var customer = CustomerRepositry.CustomerDetail();
            return Ok(customer);
        }

        [HttpGet]
        [Route("CustomerDropDown")]
        public IActionResult CustomerDropDownList()
        {
            var customer = CustomerRepositry.CustomerDropDown();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var Customer = CustomerRepositry.CustomerById(id);
            if (Customer == null)
            {
                return NotFound(new { Message = $"Customer with ID {id} not found." });
            }
            return Ok(Customer);
        }

        [HttpPost]
        public IActionResult InsertCustomer([FromBody] CustomerModel Customer)
        {
            var validator = new CustomerValidator();
            var results = validator.Validate(Customer);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Customer == null)
            {
                return BadRequest();
            }

            bool isInserted = CustomerRepositry.InsertCustomer(Customer);

            if (isInserted)
            {
                return Ok(new { Message = "Customer Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerModel Customer)
        {
            var validator = new CustomerValidator();
            var results = validator.Validate(Customer);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Customer == null || id != Customer.CustomerId)
            {
                return BadRequest();
            }

            var isUpdated = CustomerRepositry.UpdateCustomer(Customer);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid Customer ID provided." });
            }

            bool isDeleted = CustomerRepositry.DeleteCustomer(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "Customer deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the Customer." });
        }
    }
}
