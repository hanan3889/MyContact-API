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

        // GET: api/Services/get/name/{serviceName}
        [HttpGet]
        [Route("get/name/{serviceName}")]
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
    }
}
