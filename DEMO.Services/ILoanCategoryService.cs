using DEMO.Data.Entities;
using DEMO.Dto;
using DEMO.Dto.Listing;
using DEMO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Services
{
    public interface ILoanCategoryService
    {
        Task<bool> AddLoanCategoryAsync(LoanCategoryDto dto);
        Task<PaginatedResult<LoanCategoryDto>> GetLoanCategoryListAsync(LoanCategoryPaginationFilterDto filter);
        Task<bool?> ToggleStatusAsync(int id);
        Task<LoanCategory> GetLoanCategoryByIdAsync(int id);
        //Task<bool> IsVehicleNumberExistsForOtherAsync(int? id, string vehicleNumber);
        Task<bool> UpdateLoanCategoryAsync(LoanCategoryDto dto);
    }
    public class LoanCategoryService : ILoanCategoryService
    {
        private readonly ILoanCategoryRepository _LoanCategoryRepository;
        private readonly IHostEnvironment _environment;

        public LoanCategoryService(ILoanCategoryRepository LoanCategoryRepository, IHostEnvironment environment)
        {
            _LoanCategoryRepository = LoanCategoryRepository;
            _environment = environment;
        }

        public async Task<bool> AddLoanCategoryAsync(LoanCategoryDto dto)
        {
            var entity = new LoanCategory
            {
                CategoryName = dto.CategoryName,
                SubCategoryName = dto.SubCategoryName,
                CreatedDate = dto.CreatedDate,
                LogoPath = await SaveFile(dto.LogoFile),
                IsActive = true,
            };

            return await _LoanCategoryRepository.AddLoanCategoryAsync(entity);
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadPath = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", "loancategories");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/loancategories/" + fileName;
        }
        public async Task<PaginatedResult<LoanCategoryDto>> GetLoanCategoryListAsync(LoanCategoryPaginationFilterDto filter)
        {
            return await _LoanCategoryRepository.GetLoanCategoryListAsync(filter);
        }

        public async Task<bool?> ToggleStatusAsync(int id)
        {
            var entity = await _LoanCategoryRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            entity.IsActive = !entity.IsActive;
            await _LoanCategoryRepository.UpdateAsync(entity);

            return entity.IsActive;
        }
        public async Task<LoanCategory> GetLoanCategoryByIdAsync(int id)
        {
            return await _LoanCategoryRepository.GetLoanCategoryByIdAsync(id);
        }
        public async Task<bool> UpdateLoanCategoryAsync(LoanCategoryDto dto)
        {
            var entity = await _LoanCategoryRepository.GetLoanCategoryByIdAsync(dto.Id);
            if (entity == null)
                return false;
            var originalCreatedDate = entity.CreatedDate;
            // Update properties
            entity.CategoryName = dto.CategoryName;
            entity.SubCategoryName = dto.SubCategoryName;
            //entity.CreatedDate = dto.CreatedDate;
            entity.IsActive = dto.IsActive ?? true;

            if (dto.LogoFile != null)
                entity.LogoPath = await SaveFile(dto.LogoFile);
            else if (!string.IsNullOrEmpty(dto.ExistingLogoPath))
                entity.LogoPath = dto.ExistingLogoPath;

            // 🛡️ Assign the original CreatedDate back to the entity
            entity.CreatedDate = originalCreatedDate;

            return await _LoanCategoryRepository.UpdateLoanCategoryAsync(entity);
        }
    }
}
