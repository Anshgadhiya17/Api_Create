using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepositry UserRepositry;

        public UserController(UserRepositry _UserRepositry)
        {
            UserRepositry = _UserRepositry;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var user = UserRepositry.UserDetail();
            return Ok(user);
        }

        [HttpGet]
        [Route("UserDropDown")]
        public IActionResult UserDropDownlist()
        {
            var user = UserRepositry.UserDropDown();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var User = UserRepositry.UserById(id);
            if (User == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }
            return Ok(User);
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] UserModel User)
        {
            var validator = new UserValidator();
            var results = validator.Validate(User);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (User == null)
            {
                return BadRequest();
            }

            bool isInserted = UserRepositry.InsertUser(User);

            if (isInserted)
            {
                return Ok(new { Message = "User Inserted Successfully!! " });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel User)
        {
            var validator = new UserValidator();
            var results = validator.Validate(User);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (User == null || id != User.UserID)
            {
                return BadRequest();
            }

            var isUpdated = UserRepositry.UpdateUser(User);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid User ID provided." });
            }

            bool isDeleted = UserRepositry.DeleteUser(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "User deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the User." });
        }
    }
}
