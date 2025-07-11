using Microsoft.AspNetCore.Mvc;
using PersonsApi.Entities;
using PersonsApi.Repositories;
using PersonsApi.Services;

namespace PersonsApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonsController : Controller
    {
        //private readonly IPersonRepository _personRepository;

        private readonly IPersonService _employeeService;

        public PersonsController(IPersonService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("get-persons")]
        public ActionResult<List<Person>> GetPersons()
        {
            try
            {
                return Ok(_employeeService.GetEmployees());
            }
            catch (Exception e)
            {
                return NotFound("Person list could not be selected");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _employeeService.GetEmployee(id);
            if (person == null)
                return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public ActionResult<Person> CreatePerson([FromBody] Person person)
        {
            _employeeService.CreatePerson(person);
            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person person)
        {
            if (id != person.Id)
                return BadRequest();
            var existing = _employeeService.GetEmployee(id);
            if (existing == null)
                return NotFound();
            _employeeService.UpdatePerson(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var existing = _employeeService.GetEmployee(id);
            if (existing == null)
                return NotFound();
            _employeeService.DeletePerson(id);
            return NoContent();
        }
    }
}