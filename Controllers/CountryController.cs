using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepositry countryRepositry;

        public CountryController(CountryRepositry _countryRepositry)
        {
            countryRepositry = _countryRepositry;
        }

        [HttpGet]
        public IActionResult GetAllCity()
        {
            var countries = countryRepositry.CountryDetail();
            return Ok(countries);
        }

        [HttpPost]
        public IActionResult InsertCountry([FromBody] CountryModel country)
        {
            var validator = new CountryValidator();
            var results = validator.Validate(country);
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (country == null)
            {
                return BadRequest();
            }

            bool isInserted = (countryRepositry.InsertCountry(country));

            if (isInserted)
            {
                return Ok(new { Message = "Country Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid Country ID provided." });
            }

            bool isDeleted = countryRepositry.DeleteCountry(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "Country deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the Country." });
        }
        [HttpGet]
        [Route("CountryDropDown")]
        public IActionResult CountryDropDownList()
        {
            var countries = countryRepositry.CountryDropDown();
            return Ok(countries);
        }
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = countryRepositry.CountryByID(id);
            if (country == null)
            {
                return NotFound(new { Message = $"Country with ID {id} not found." });
            }
            return Ok(country);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryModel country)
        {
            var validator = new CountryValidator();
            var results = validator.Validate(country);
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (country == null || id != country.CountryID)
            {
                return BadRequest();
            }

            var isUpdated = countryRepositry.UpdateCountry(country);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

    }
}
