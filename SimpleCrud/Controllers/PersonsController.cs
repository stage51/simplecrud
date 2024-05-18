using SimpleCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private static List<Person> Persons = new List<Person>
        {
            new Person { Id = 1, Name = "John Doe", Age = 30 },
            new Person { Id = 2, Name = "Jane Doe", Age = 25 }
        };

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return Persons;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            var person = Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpPost]
        public ActionResult<Person> Post(Person person)
        {
            person.Id = Persons.Max(p => p.Id) + 1;
            Persons.Add(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Person person)
        {
            var existingPerson = Persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson == null)
            {
                return NotFound();
            }
            existingPerson.Name = person.Name;
            existingPerson.Age = person.Age;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Persons.Remove(person);
            return NoContent();
        }
    }
}


