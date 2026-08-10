using e_commerce_Api.Data;
using e_commerce_Api.Models;
using e_commerce_Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public AuthController(
            JwtService jwtService,
            AppDbContext context,
            PasswordService passwordService)
        {
            _jwtService = jwtService;
            _context = context;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest request)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUser != null)
            {
                return BadRequest(new
                {
                    message = "Username already exists"
                });
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = _passwordService.HashPassword(request.Password)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully",
                username = user.Username
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            var passwordValid = _passwordService.VerifyPassword(
                request.Password,
                user.PasswordHash
            );

            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            var token = await _jwtService.GenerateToken(user.Username);

            return Ok(new
            {
                token = token,
                expiresIn = "1 hour"
            });
        }
    }
}