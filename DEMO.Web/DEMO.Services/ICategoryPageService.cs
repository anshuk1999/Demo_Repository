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
    public interface ICategoryPageService
    {
        Task<bool> AddCategoryPageAsync(CategoryPageDto dto);
        Task<List<CategoryName>> GetAllCategoriesAsync(); // <-- Add this
        //Task<CategoryName> GetByIdAsync(int id);
    }
    public class CategoryPageService : ICategoryPageService
    {
        private readonly ICategoryPageRepository _CategoryPageRepository;
        private readonly IHostEnvironment _environment;

        public CategoryPageService(ICategoryPageRepository CategoryPageRepository, IHostEnvironment environment)
        {
            _CategoryPageRepository = CategoryPageRepository;
            _environment = environment;
        }
        public async Task<bool> AddCategoryPageAsync(CategoryPageDto dto)
        {
            var entity = new CategoryName
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
                Status = true,
            };

            return await _CategoryPageRepository.AddCategoryPageAsync(entity);
        }
        public async Task<List<CategoryName>> GetAllCategoriesAsync()
        {
            return await _CategoryPageRepository.GetAllCategoriesAsync();
        }

    }
}
