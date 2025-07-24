using DEMO.Data;
using DEMO.Data.Entities;
using DEMO.Dto;
using DEMO.Dto.Listing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Repository
{
    public interface ILoanCategoryRepository
    {
        Task<bool> AddLoanCategoryAsync(LoanCategory entity);
        Task<PaginatedResult<LoanCategoryDto>> GetLoanCategoryListAsync(LoanCategoryPaginationFilterDto filter);
        Task<LoanCategory> GetByIdAsync(int id);
        Task UpdateAsync(LoanCategory user);
        Task<LoanCategory> GetLoanCategoryByIdAsync(int? id);
        //Task<bool> IsVehicleNumberExistsForOtherAsync(int? id, string vehicleNumber);
        Task<bool> UpdateLoanCategoryAsync(LoanCategory entity);
    }

    public class LoanCategoryRepository : ILoanCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddLoanCategoryAsync(LoanCategory entity)
        {
            await _context.LoanCategories.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResult<LoanCategoryDto>> GetLoanCategoryListAsync(LoanCategoryPaginationFilterDto filter)
        {
            var query = _context.LoanCategories.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(x => x.CategoryName.Contains(filter.SearchTerm));
            }
            // ✅ NEW CATEGORY FILTER
            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                query = query.Where(x => x.CategoryName == filter.CategoryName);
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.CreatedDate >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(x => x.CreatedDate <= filter.ToDate.Value);
            }

            // Sorting
            if (filter.SortColumn == "CategoryName")
            {
                query = filter.SortDirection == "asc" ? query.OrderBy(x => x.CategoryName) : query.OrderByDescending(x => x.CategoryName);
            }
            else // Default: CreatedDate
            {
                query = filter.SortDirection == "asc" ? query.OrderBy(x => x.CreatedDate) : query.OrderByDescending(x => x.CreatedDate);
            }

            int total = await query.CountAsync();
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new LoanCategoryDto
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    SubCategoryName = x.SubCategoryName,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    ExistingLogoPath = x.LogoPath
                }).ToListAsync();

            return new PaginatedResult<LoanCategoryDto>(items, total);

        }

        public async Task<LoanCategory> GetByIdAsync(int id)
        {
            return await _context.LoanCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(LoanCategory entity)
        {
            _context.LoanCategories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<LoanCategory> GetLoanCategoryByIdAsync(int? id)
        {
            return await _context.LoanCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateLoanCategoryAsync(LoanCategory entity)
        {
            _context.LoanCategories.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
