using DEMO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Repository
{
    public interface ISubCategoryPageRepository
    {
        Task<bool> AddCategoryPageAsync(SubCategoryName entity);
        //Task<CategoryName> GetByIdAsync(int id);
    }
    public class SubCategoryPageRepository : ISubCategoryPageRepository
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryPageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategoryPageAsync(SubCategoryName entity)
        {
            await _context.SubCategoryNames.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
            //return entity.Id; // EF automatically fills this after SaveChanges
        }

    }
}
