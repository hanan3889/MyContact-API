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
    public class SitesController : ControllerBase
    {
        private readonly MyContactDbContext _context;

        public SitesController(MyContactDbContext context)
        {
            _context = context;
        }

        //GET: api/Sites (Récupérer tous les sites)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sites>>> GetSites()
        {
            return await _context.Sites.ToListAsync();
        }

        //GET: api/Sites/{id} (Récupérer un site par ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Sites>> GetSite(int id)
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        //POST: api/Sites (Créer un nouveau site)
        [HttpPost]
        public async Task<ActionResult<Sites>> CreateSite([FromBody] Sites site)
        {
            if (site == null)
                return BadRequest("Données invalides.");

            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSite), new { id = site.Id }, site);
        }


        //PUT: api/Sites/{id} (Mettre à jour un site)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSite(int id, Sites site)
        {
            if (id != site.Id)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE: api/Sites/{id} (Supprimer un site)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GET: api/Sites/get/name/{ville} (Récupérer les salariés par ville)
        [HttpGet("get/name/{ville}")]
        public async Task<ActionResult<IEnumerable<Salaries>>> GetSalariesByCity(string ville)
        {
            var salaries = await _context.Salaries
                 .Include(s => s.Service)
                 .Include(s => s.Site)
                 .Where(s => s.Site.Ville == ville)
                 .ToListAsync();

            if (!salaries.Any())
            {
                return NotFound();
            }

            return Ok(salaries);
        }

        //Vérifie si un site existe
        private bool SiteExists(int id)
        {
            return _context.Sites.Any(e => e.Id == id);
        }
    }
}
