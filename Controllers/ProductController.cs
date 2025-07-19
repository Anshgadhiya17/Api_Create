using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepositry ProductRepositry;

        public ProductController(ProductRepositry _ProductRepositry)
        {
            ProductRepositry = _ProductRepositry;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var product = ProductRepositry.ProductDetail();
            return Ok(product);
        }

        [HttpGet]
        [Route("ProductDropDown")]
        public IActionResult ProductDropDownList()
        {
            var product = ProductRepositry.ProductDropDown();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var Product = ProductRepositry.ProductById(id);
            if (Product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }
            return Ok(Product);
        }

        [HttpPost]
        public IActionResult InsertProduct([FromBody] ProductModel Product)
        {
            var validator = new ProductValidator();
            var results = validator.Validate(Product);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Product == null)
            {
                return BadRequest();
            }

            bool isInserted = ProductRepositry.InsertProduct(Product);

            if (isInserted)
            {
                return Ok(new { Message = "Product Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductModel Product)
        {
            var validator = new ProductValidator();
            var results = validator.Validate(Product);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (Product == null || id != Product.ProductID)
            {
                return BadRequest();
            }

            var isUpdated = ProductRepositry.UpdateProduct(Product);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid Product ID provided." });
            }

            bool isDeleted = ProductRepositry.DeleteProduct(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "Product deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the Product." });
        }
    }
}
