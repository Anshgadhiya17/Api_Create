using ApiDemo.Data;
using ApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepositry stateRepositry;

        public StateController(StateRepositry _stateRepositry)
        {
            stateRepositry = _stateRepositry;
        }

        [HttpGet]
        public IActionResult GetAllState()
        {
            var states = stateRepositry.StateDetail();
            return Ok(states);
        }

        [HttpPost]
        public IActionResult InsertState([FromBody] StateModel State)
        {
            var validator = new StateValidator();
            var results = validator.Validate(State);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (State == null)
            {
                return BadRequest();
            }

            bool isInserted = stateRepositry.InsertState(State);

            if (isInserted)
            {
                return Ok(new { Message = "State Inserted Successfully!! " });
            }

            return StatusCode(500);
        }
        [HttpGet("ComboBox/{Id}")]
        public IActionResult SelectComboBoxByCountryIDList(int Id)
        {
            var state = stateRepositry.SelectComboBoxByCountryID(Id);
            if (state == null)
            {
                return NotFound(new { Message = $"State with ID {Id} not found." });
            }
            return Ok(state);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] StateModel State)
        {
            var validator = new StateValidator();
            var results = validator.Validate(State);

            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    Console.WriteLine($"Property {error.PropertyName}: {error.ErrorMessage}");
                }
            }
            if (State == null || id != State.StateID)
            {
                return BadRequest();
            }

            var isUpdated = stateRepositry.UpdateState(State);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();


        }

        [HttpGet]
        [Route("StateDropDown")]
        public IActionResult StateDropDownList()
        {
            var state = stateRepositry.StateDropDown();
            return Ok(state);
        }
        [HttpGet("{id}")]
        public IActionResult GetStateByID(int id)
        {
            var state = stateRepositry.StateById(id);
            if (state == null)
            {
                return NotFound(new { Message = $"State with ID {id} not found." });
            }
            return Ok(state);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid State ID provided." });
            }

            bool isDeleted = stateRepositry.DeleteState(id);

            if (!isDeleted)
            {
                return Ok(new { Message = "State deleted successfully!" });
            }

            return StatusCode(500, new { Message = "An error occurred while deleting the State." });
        }
    }
}
