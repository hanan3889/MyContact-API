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
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || user.SecretCode == 0)
            {
                return BadRequest("Les données de l'utilisateur ne sont pas valides.");
            }

            // Vérifier si l'email existe déjà
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("L'email existe déjà.");
            }

            // Hacher le mot de passe
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login([FromBody] UserLoginModel userModel)
        {
            if (userModel == null || string.IsNullOrWhiteSpace(userModel.Email) || string.IsNullOrWhiteSpace(userModel.Password) || userModel.SecretCode == 0)
            {
                return BadRequest("Les données de l'utilisateur ne sont pas valides.");
            }

            // Trouver l'utilisateur par email
            var storedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userModel.Email);
            if (storedUser == null)
            {
                return Unauthorized("Email ou mot de passe incorrect.");
            }

            // Vérifier le mot de passe
            if (!BCrypt.Net.BCrypt.Verify(userModel.Password, storedUser.Password))
            {
                return Unauthorized("Email ou mot de passe incorrect.");
            }

            // Vérifier le code secret
            if (userModel.SecretCode != storedUser.SecretCode)
            {
                return Unauthorized("Code secret invalide.");
            }

            return Ok(storedUser);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Users>> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int SecretCode { get; set; }
    }
}