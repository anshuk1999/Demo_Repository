using DEMO.Data.Entities;
using DEMO.Dto;
using DEMO.Dto.Listing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Repository
{
    public interface ICategoryPageRepository
    {
        Task<bool> AddCategoryPageAsync(CategoryName entity);
        Task<List<CategoryName>> GetAllCategoriesAsync();

        //Task<CategoryName> GetByIdAsync(int id);
    }
    public class CategoryPageRepository : ICategoryPageRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryPageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategoryPageAsync(CategoryName entity)
        {
            await _context.CategoryNames.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
            //return entity.Id; // EF automatically fills this after SaveChanges
        }
        public async Task<List<CategoryName>> GetAllCategoriesAsync()
        {
            return await _context.CategoryNames.Where(c => c.Status).ToListAsync();
        }

    }
}