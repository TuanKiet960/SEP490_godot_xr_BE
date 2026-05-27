using godotxr_SEP490.DTOs;
using godotxr_SEP490.Models;
using godotxr_SEP490.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace godotxr_SEP490.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly GodotxrDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(GodotxrDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.UsernameOrEmail) ||
                string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin." });

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Email == request.UsernameOrEmail ||
                    u.FullName == request.UsernameOrEmail);

            if (user == null)
                return Unauthorized(new { message = "Tài khoản không tồn tại." });

            if (user.Status != "active")
                return Unauthorized(new { message = "Tài khoản đã bị khoá." });

            var hashedInput = HashPassword(request.Password);
            if (user.PasswordHash != hashedInput)
                return Unauthorized(new { message = "Mật khẩu không đúng." });
            var token = _jwtService.GenerateToken(user, user.Role.RoleName);

            return Ok(new LoginResponseDTO
            {
                Token = token,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.RoleName,
                ExpiresAt = DateTime.UtcNow.AddHours(8)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName))
                return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin." });

            var exists = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (exists)
                return Conflict(new { message = "Email đã được sử dụng." });
            var defaultRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == "User");
            if (defaultRole == null)
                return StatusCode(500, new { message = "Lỗi hệ thống: không tìm thấy role mặc định." });

            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                RoleId = defaultRole.RoleId,
                Status = "active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đăng ký thành công." });
        }
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}