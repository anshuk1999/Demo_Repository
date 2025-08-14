using DEMO.Data; // Your DbContext namespace
using DEMO.Data.Entities;
using DEMO.Dto; // Your DTO namespace
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Repository
{
    public interface ICommonRepository
    {
        Task<List<CategoryPageDto>> GetAllCategoriesAsync();
        Task<List<SubCategoryPageDto>> GetAllSubCategoriesAsync();
    }

    public class CommonRepository : ICommonRepository
    {
        private readonly ApplicationDbContext _context;

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all categories
        public async Task<List<CategoryPageDto>> GetAllCategoriesAsync()
        {
            return await _context.CategoryNames
                .Select(c => new CategoryPageDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        // Get all subcategories
        public async Task<List<SubCategoryPageDto>> GetAllSubCategoriesAsync()
        {
            return await _context.SubCategoryNames
                .Select(sc => new SubCategoryPageDto
                {
                    Id = sc.Id,
                    SubName = sc.SubName,
                    CategoryId = sc.CategoryId  // ← include this
                })
                .ToListAsync();
        }
    }
}
