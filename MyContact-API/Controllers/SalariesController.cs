using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;

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
        public async Task<ActionResult<IEnumerable<Salaries>>> GetAll()
        {
            var data = await _context.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .ToListAsync();

            foreach (var salary in data)
            {
                if (salary.SiteId == 1)
                {
                    salary.ServiceId = 1; // Siège administratif
                    salary.Service = new Services { Id = 1, Nom = "Siège administratif" };
                }
                else
                {
                    salary.ServiceId = 2; // Production
                    salary.Service = new Services { Id = 2, Nom = "Production" };
                }
            }

            if (data.Any())
            {
                return Ok(data);
            }

            return NotFound();
        }

        // GET: api/Salaries/get/{id}
        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<SalariesDto>> GetById(int id)
        {
            var salary = await _context.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (salary == null)
            {
                return NotFound();
            }

            var salaryDto = new SalariesDto
            {
                Id = salary.Id,
                Nom = salary.Nom,
                Prenom = salary.Prenom,
                TelephoneFixe = salary.TelephoneFixe,
                TelephonePortable = salary.TelephonePortable,
                Email = salary.Email,
                ServiceId = salary.ServiceId,
                ServiceNom = salary.Service.Nom,
                SiteId = salary.SiteId,
                SiteVille = salary.Site.Ville
            };

            return Ok(salaryDto);
        }

        // GET: api/Salaries/get/name/{nom}
        [HttpGet]
        [Route("get/name/{nom}")]
        public async Task<ActionResult<IEnumerable<SalariesDto>>> GetByName(string nom)
        {
            var salaries = await _context.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .Where(s => s.Nom.Contains(nom))
                .ToListAsync();

            if (!salaries.Any())
            {
                return NotFound();
            }

            var salariesDto = salaries.Select(salary => new SalariesDto
            {
                Id = salary.Id,
                Nom = salary.Nom,
                Prenom = salary.Prenom,
                TelephoneFixe = salary.TelephoneFixe,
                TelephonePortable = salary.TelephonePortable,
                Email = salary.Email,
                ServiceId = salary.ServiceId,
                ServiceNom = salary.Service.Nom,
                SiteId = salary.SiteId,
                SiteVille = salary.Site.Ville
            }).ToList();

            return Ok(salariesDto);
        }

        // POST: api/Salaries/create
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Salaries>> Create([FromBody] Salaries data)
        {
            if (data == null)
            {
                return BadRequest("Salary data is null.");
            }

            if (data.ServiceId == 0 || data.SiteId == 0)
            {
                return BadRequest("ServiceId and SiteId must be provided.");
            }

            // Vérifiez l'existence du ServiceId
            var serviceExists = await _context.Services.AnyAsync(s => s.Id == data.ServiceId);
            if (!serviceExists)
            {
                return BadRequest("ServiceId does not exist.");
            }

            // Vérifiez l'existence du SiteId
            var siteExists = await _context.Sites.AnyAsync(s => s.Id == data.SiteId);
            if (!siteExists)
            {
                return BadRequest("SiteId does not exist.");
            }

            try
            {
                _context.Salaries.Add(data);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = data.Id }, data);
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }
                return StatusCode(500, "An error occurred while saving the entity changes. See the inner exception for details.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Salaries/update/{id}
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Salaries salary)
        {
            if (salary == null || salary.Id != id)
            {
                return BadRequest();
            }

            var existingSalary = await _context.Salaries.FindAsync(id);
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
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Salaries/delete/{id}
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            try
            {
                _context.Salaries.Remove(salary);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
