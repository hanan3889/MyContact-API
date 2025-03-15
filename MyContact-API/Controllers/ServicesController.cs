using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyContact_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly MyContactDbContext _context;

        public ServicesController(MyContactDbContext context)
        {
            _context = context;
        }

        // 📌 Récupérer tous les services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetAllServices()
        {
            var services = await _context.Services.ToListAsync();
            return Ok(services);
        }

        // 📌 Récupérer un service par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Services>> GetServiceById(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        //Récupérer les salariés d'un service par nom
        [HttpGet("get/name/{serviceName}")]
        public async Task<ActionResult<IEnumerable<Salaries>>> GetSalariesByService(string serviceName)
        {
            var salaries = await _context.Salaries
                 .Include(s => s.Service)
                 .Include(s => s.Site)
                 .Where(s => s.Service.Nom == serviceName)
                 .ToListAsync();

            if (!salaries.Any())
            {
                return NotFound();
            }

            return Ok(salaries);
        }

        //Ajouter un nouveau service
        [HttpPost]
        public async Task<ActionResult<Services>> CreateService(Services service)
        {
            if (service == null || string.IsNullOrEmpty(service.Nom))
            {
                return BadRequest("Le nom du service est requis.");
            }

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }

        //Modifier un service existant
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, Services updatedService)
        {
            if (id != updatedService.Id)
            {
                return BadRequest("L'ID du service ne correspond pas.");
            }

            var existingService = await _context.Services.FindAsync(id);
            if (existingService == null)
            {
                return NotFound();
            }

            existingService.Nom = updatedService.Nom;

            _context.Entry(existingService).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Supprimer un service
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
