using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace BusinessObjects.IRepositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string roleName);
    }
}