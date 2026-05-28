using BusinessObjects.IRepositories;
using DataAccess.DBContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GodotxrDbContext _context;

        public UserRepository(GodotxrDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Email == emailOrUsername ||
                    u.FullName == emailOrUsername);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}