using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;

namespace MyContact_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly MyContactDbContext _context;

        public SitesController(MyContactDbContext context)
        {
            _context = context;
        }

        // GET: api/Sites/get/name/{ville}
        [HttpGet]
        [Route("get/name/{ville}")]
        public async Task<ActionResult<IEnumerable<Salaries>>> GetSalariesByCity(string ville)
        {
            var salaries = await _context.Salaries
                 .Include(s => s.Service)  // Charger la relation Service
                 .Include(s => s.Site)  // Charger la relation Site
                 .Where(s => s.Site.Ville == ville)
                 .ToListAsync();
                

            if (!salaries.Any())
            {
                return NotFound();
            }

            return Ok(salaries);
        }

       
    }
}