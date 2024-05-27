using SimpleCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> Students = new List<Student>
        {
            new Student { Id = 1, Name = "Александр", Age = 23, Hobby = "Рисование" },
            new Student { Id = 2, Name = "Михаил", Age = 25, Hobby = "Музыка" }
        };

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return Students;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = Students.FirstOrDefault(p => p.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public ActionResult<Student> Post(Student student)
        {
            student.Id = Students.Max(p => p.Id) + 1;
            Students.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Student student)
        {
            var existingStudent = Students.FirstOrDefault(p => p.Id == id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = Students.FirstOrDefault(p => p.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            Students.Remove(student);
            return NoContent();
        }
    }
}


