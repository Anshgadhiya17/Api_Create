using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepositry cityRepositry;

        public CityController(CityRepositry _cityRepositry)
        {
            cityRepositry = _cityRepositry;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cities = cityRepositry.CityDetail();
            return Ok(cities);
        }

        [HttpPost]
        public IActionResult InsertCity(CityModel city)
        {
            var validator = new CityValidator();
            var results = validator.Validate(city);
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (city == null)
            {
                return BadRequest();

            }
            bool isInserted = cityRepositry.Insert(city);
            if (isInserted)
            {
                return Ok();

            }
            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityModel city)
        {
            var validator = new CityValidator();
            var results = validator.Validate(city);
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (city == null || id != city.CityID)
            {
                return BadRequest();
            }

            var isUpdated = cityRepositry.UpdateCity(city);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid city ID provided." });
            }

            bool isDeleted = cityRepositry.DeleteCity(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "City deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the city." });
        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = cityRepositry.CityById(id);
            if (city == null)
            {
                return NotFound(new { Message = $"City with ID {id} not found." });
            }
            return Ok(city);
        }

    }
}
