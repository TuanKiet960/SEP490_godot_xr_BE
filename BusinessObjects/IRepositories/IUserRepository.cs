using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername);
        Task<bool> EmailExistsAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
