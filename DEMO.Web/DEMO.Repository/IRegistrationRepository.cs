using DEMO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Repository
{
    public interface IRegistrationRepository
    {
        Task<bool> AddRegistrationAsync(Registration entity);
        //Task<CategoryName> GetByIdAsync(int id);
    }
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public RegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRegistrationAsync(Registration entity)
        {
            await _context.Registrations.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
            //return entity.Id; // EF automatically fills this after SaveChanges
        }

    }
}
