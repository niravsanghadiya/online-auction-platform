

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuctionPlatform.Domain.Entities;
using OnlineAuctionPlatform.Infrastructure.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly AuctionContext _context;

        public UserAccountController(AuctionContext context)
        {
            _context = context;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Email already in use.");
            }

            // Hash the password
            user.PasswordHash = HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.Id, user.UserName, user.Email });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if (user == null || !VerifyPassword(loginUser.PasswordHash, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { user.Id, user.UserName, user.Email });
        }

        private string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (var hmac = new HMACSHA512())
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                return Convert.ToBase64String(hash) == storedHash;
            }
        }
    }
}
