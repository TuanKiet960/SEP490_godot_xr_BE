using BusinessObjects.IRepositories;
using DataAccess.DBContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly GodotxrDbContext _context;

        public RoleRepository(GodotxrDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
    }
}