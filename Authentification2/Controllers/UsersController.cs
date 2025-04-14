using Authentification2.Models;
using Authentification2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public UsersController(ApplicationDBContext context, IConfiguration configuration, EmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {

            if (user == null)
                return BadRequest(new { detail = "Invalid user data." });

            if (string.IsNullOrWhiteSpace(user.Email))
                return BadRequest(new { detail = "Email is required." });

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                return BadRequest(new { detail = "Password is required." });

            if (string.IsNullOrWhiteSpace(user.Name))
                return BadRequest(new { detail = "Name is required." });

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest(new { detail = "Email already exists." });


            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var subject = "Welcome to BitBot Enterprise!";
            var logoUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.designcrowd.com%2Fdesign%2F1904994&psig=AOvVaw2arplkXLNI3xkcZL__l-5D&ust=1744631978889000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCKCktuL61IwDFQAAAAAdAAAAABAE"; // Replace with your actual logo URL

            var body = $"Hello {user.Name},\n\nThank you for registering!";

            // Replace placeholders
            body = body.Replace("{{Name}}", user.Name)
                       .Replace("https://yourdomain.com/images/bitbot-logo-white.png", logoUrl);

            await _emailService.SendEmailAsync(user.Email, subject, body, isBodyHtml: true);

            return Ok("User registered successfully. A welcome email has been sent.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                return StatusCode(401, new { detail = "Email not found" });
            }

            if (!BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash))
            {
                return StatusCode(401, new { detail = "Incorrect password" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { user.Email, token });
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpGet("getUsers")]
        public IActionResult getAllUsers()
        {
            var list = _context.Users.ToList();
            return Ok(list);
        }


    }
}
