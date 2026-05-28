using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.IRepositories;
using DataAccess.Models;

namespace BusinessObjects.Dao
{
    public class AuthDao
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;

        public AuthDao(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task<User?> ValidateLoginAsync(string emailOrUsername, string hashedPassword)
        {
            var user = await _userRepo.GetByEmailOrUsernameAsync(emailOrUsername);

            if (user == null) return null;
            if (user.Status != "active") return null;
            if (user.PasswordHash != hashedPassword) return null;

            return user;
        }

        public async Task<(bool Success, string Message, User? User)> RegisterAsync(
            string fullName, string email, string hashedPassword, string? phoneNumber)
        {
            var exists = await _userRepo.EmailExistsAsync(email);
            if (exists)
                return (false, "Email đã được sử dụng.", null);

            var defaultRole = await _roleRepo.GetByNameAsync("User");
            if (defaultRole == null)
                return (false, "Lỗi hệ thống: không tìm thấy role mặc định.", null);

            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hashedPassword,
                PhoneNumber = phoneNumber,
                RoleId = defaultRole.RoleId,
                Status = "active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _userRepo.CreateAsync(newUser);
            return (true, "Đăng ký thành công.", created);
        }
    }
}
