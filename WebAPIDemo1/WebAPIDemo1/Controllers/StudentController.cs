using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo1.DataAccessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIDemo1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentController>
        public List<Student> Students;
        private static 
            IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
            Students = new List<Student>
        {
            new Student("John Doe", 20, "Computer Science", 3.5),
            new Student("Jane Smith", 22, "Mathematics", 3.9),
            new Student("Alice Johnson", 21, "Biology", 3.2)
        };
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet]
        [Route("GetStudentDetails")]
        public IEnumerable<Student> GetStudentDetails()
        {
            // return new string[] { "value1", "value2" };
            DAL OBJ = new DAL();
          return  OBJ.GetStudents();
        }


        // GET api/<StudentController>/5
        [HttpGet("{Name}")]
        public Student GetStudent(string Name)
        {
          return  Students.FirstOrDefault(x=> x.Name==Name);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] Student value)
        {
            //  Students.Add(value);
            DAL OBJ = new DAL();
            OBJ.InsertStudent(value);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
