using ApiDemo.Data;
using ApiDemo.Models;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly BillsRepositry BillsRepositry;

        public BillsController(BillsRepositry _BillsRepositry)
        {
            BillsRepositry = _BillsRepositry;
        }

        [HttpGet]
        public IActionResult GetAllBills()
        {
            var bills = BillsRepositry.BillDetail();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public IActionResult GetBillsById(int id)
        {
            var Bills = BillsRepositry.BillById(id);
            if (Bills == null)
            {
                return NotFound(new { Message = $"Bills with ID {id} not found." });
            }
            return Ok(Bills);
        }

        [HttpPost]
        public IActionResult InsertBills([FromBody] BillsModel Bills)
        {

            var validator = new BillsValidator();
            var results = validator.Validate(Bills);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Bills == null)
            {
                return BadRequest();
            }

            bool isInserted = BillsRepositry.InsertBill(Bills);

            if (isInserted)
            {
                return Ok(new { Message = "Bills Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBills(int id, [FromBody] BillsModel Bills)
        {
            var validator = new BillsValidator();
            var results = validator.Validate(Bills);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Bills == null || id != Bills.BillID)
            {
                return BadRequest();
            }

            var isUpdated = BillsRepositry.UpdateBill(Bills);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBills(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid Bills ID provided." });
            }

            bool isDeleted = BillsRepositry.DeleteBill(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "Bills deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the Bills." });
        }
    }
}
