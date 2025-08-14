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
    public interface ISubCategoryPageService
    {
        Task<bool> AddCategoryPageAsync(SubCategoryPageDto dto);
        //Task<CategoryName> GetByIdAsync(int id);
    }
    public class SubCategoryPageService : ISubCategoryPageService
    {
        private readonly ISubCategoryPageRepository _subCategoryPageRepository;
        private readonly ICategoryPageRepository _categoryPageRepository;

        public SubCategoryPageService(ISubCategoryPageRepository subCategoryRepo,
                                      ICategoryPageRepository categoryRepo)
        {
            _subCategoryPageRepository = subCategoryRepo;
            _categoryPageRepository = categoryRepo;
        }

        public async Task<List<CategoryName>> GetAllCategoriesAsync()
        {
            // You create this method in CategoryPageRepository to get all categories
            return await _categoryPageRepository.GetAllCategoriesAsync();
        }

        // Your existing add method:
        public async Task<bool> AddCategoryPageAsync(SubCategoryPageDto dto)
        {
            var entity = new SubCategoryName
            {
                SubName = dto.SubName,
                CreatedDate = DateTime.Now,
                Status = true,
                CategoryId = dto.CategoryId
            };

            return await _subCategoryPageRepository.AddCategoryPageAsync(entity);
        }
    }

}
