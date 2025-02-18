using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContact_API.Models;
using BCrypt;

namespace MyContact_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyContactDbContext _context;

        public UsersController(MyContactDbContext context)
        {
            _context = context;
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register(Users user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid user data.");
            }

            // Vérifier si l'email existe déjà
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Email already exists.");
            }

            // Hacher le mot de passe
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(Users user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid user data.");
            }

            // Trouver l'utilisateur par email
            var storedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (storedUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Vérifier le mot de passe
            if (!BCrypt.Net.BCrypt.Verify(user.Password, storedUser.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(storedUser);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
