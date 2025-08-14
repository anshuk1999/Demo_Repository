using DEMO.Data.Entities;
using DEMO.Dto;
using DEMO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Services
{
    public interface IRegistrationService
    {
        Task<bool> AddRegistrationAsync(RegistrationDto dto);
        Task<List<CategoryPageDto>> GetAllCategoriesAsync();
        Task<List<SubCategoryPageDto>> GetAllSubCategoriesAsync();
        Task<List<SubCategoryPageDto>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    }
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly ICommonRepository _commonRepository;

        public RegistrationService(IRegistrationRepository registrationRepo,
                                   ICommonRepository commonRepo)
        {
            _registrationRepository = registrationRepo;
            _commonRepository = commonRepo;
        }

        public async Task<List<CategoryPageDto>> GetAllCategoriesAsync()
        {
            return await _commonRepository.GetAllCategoriesAsync();
        }
        public async Task<List<SubCategoryPageDto>> GetAllSubCategoriesAsync()
        {
            return await _commonRepository.GetAllSubCategoriesAsync();
        }
        // Your existing add method:
        public async Task<bool> AddRegistrationAsync(RegistrationDto dto)
        {
            var entity = new Registration
            {
                CategoryId = dto.CategoryId,
                SubCategoryId = dto.SubCategoryId,
                EmployeeName = dto.EmployeeName,
                EmployeeAge = dto.EmployeeAge,
                EmployeeEmail = dto.EmployeeEmail,
                EmployeeMobile = dto.EmployeeMobile,
                Address = dto.Address,
                City = dto.City,
                District = dto.District,
                State = dto.State,
                Country = dto.Country,
                EmployeeSalary = dto.EmployeeSalary
            };

            return await _registrationRepository.AddRegistrationAsync(entity);
        }
        public async Task<List<SubCategoryPageDto>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            // Get all subcategories from common repo
            var allSubCategories = await _commonRepository.GetAllSubCategoriesAsync();

            // Return only subcategories that match the selected category
            return allSubCategories.Where(sc => sc.CategoryId == categoryId).ToList();
        }


    }
}
