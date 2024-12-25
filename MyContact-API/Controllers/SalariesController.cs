using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;
using System.Linq;

namespace MyContact_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalariesController : ControllerBase
    {
        private readonly MyContactDbContext _context;

        public SalariesController(MyContactDbContext context)
        {
            _context = context;
        }

        // GET: api/Salaries/get/all
        [HttpGet]
        [Route("get/all")]
        public ActionResult GetAll()
        {
            var data = _context.Salaries.ToList();
            if (data.Any())
            {
                return Ok(data);
            }

            return NotFound();
        }

        // GET: api/Salaries/get/{id}
        [HttpGet]
        [Route("get/{id}")]
        public ActionResult GetById(int id)
        {
            var salary = _context.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            return Ok(salary);
        }

        // POST: api/Salaries/create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Salaries salary)
        {
            if (salary == null)
            {
                return BadRequest("Salary data is null.");
            }

            try
            {
                _context.Salaries.Add(salary);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = salary.Id }, salary);
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error
            }
        }

        // PUT: api/Salaries/update/{id}
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(int id, [FromBody] Salaries salary)
        {
            if (salary == null || salary.Id != id)
            {
                return BadRequest();
            }

            var existingSalary = _context.Salaries.Find(id);
            if (existingSalary == null)
            {
                return NotFound();
            }

            existingSalary.Nom = salary.Nom;
            existingSalary.Prenom = salary.Prenom;
            existingSalary.TelephoneFixe = salary.TelephoneFixe;
            existingSalary.TelephonePortable = salary.TelephonePortable;
            existingSalary.Email = salary.Email;
            existingSalary.ServiceId = salary.ServiceId;
            existingSalary.SiteId = salary.SiteId;

            try
            {
                _context.Salaries.Update(existingSalary);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error
            }
        }

        // DELETE: api/Salaries/delete/{id}
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var salary = _context.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            try
            {
                _context.Salaries.Remove(salary);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error
            }
        }
    }
}
