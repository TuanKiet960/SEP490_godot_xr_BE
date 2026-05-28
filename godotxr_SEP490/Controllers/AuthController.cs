using BusinessObjects.Dao;
using BusinessObjects.Services;
using DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace godotxr_SEP490.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDao _authDao;
        private readonly JwtService _jwtService;

        public AuthController(AuthDao authDao, JwtService jwtService)
        {
            _authDao = authDao;
            _jwtService = jwtService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.UsernameOrEmail) ||
                string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Vui lòng nhập đầy đủ thông tin." });

            var hashedPassword = PasswordHelper.Hash(request.Password);
            var user = await _authDao.ValidateLoginAsync(request.UsernameOrEmail, hashedPassword);

            if (user == null)
                return Unauthorized(new { message = "Tài khoản không tồn tại hoặc mật khẩu sai." });

            var token = _jwtService.GenerateToken(user, user.Role!.RoleName);

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

            var hashedPassword = PasswordHelper.Hash(request.Password);
            var (success, message, _) = await _authDao.RegisterAsync(
                request.FullName, request.Email, hashedPassword, request.PhoneNumber);

            if (!success)
                return Conflict(new { message });

            return Ok(new { message });
        }
    }
}