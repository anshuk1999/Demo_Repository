using DEMO.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Repository
{
    public interface IAdminLoginRepository
    {
        Task<AdminLogin?> GetByEmailAsync(string email);
    }

    public class AdminLoginRepository : IAdminLoginRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminLoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminLogin?> GetByEmailAsync(string email)
        {
            return await _context.AdminLogins
                .FirstOrDefaultAsync(x => x.EmailId == email && x.IsActive);
        }
    }
}
