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
    public interface IVehicleService
    {
        Task<bool> AddVehicleAsync(AddVehicleDto dto);
        Task<PaginatedResult<AddVehicleDto>> GetVehicleListAsync(VehiclePaginationFilterDto filter);
        Task<bool?> ToggleStatusAsync(int id);
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<bool> IsVehicleNumberExistsForOtherAsync(int? id, string vehicleNumber);
        Task<bool> UpdateVehicleAsync(AddVehicleDto dto);
    }

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IHostEnvironment _environment;

        public VehicleService(IVehicleRepository vehicleRepository, IHostEnvironment environment)
        {
            _vehicleRepository = vehicleRepository;
            _environment = environment;
        }
        public async Task<bool> AddVehicleAsync(AddVehicleDto dto)
        {
            var entity = new Vehicle
            {
                VehicleNumber = dto.VehicleNumber,
                OwnerName = dto.OwnerName,
                ChasisNumber = dto.ChasisNumber,
                EngineNumber = dto.EngineNumber,
                RegistrationDate = dto.RegistrationDate,
                Maker = dto.Maker,
                VehicleModel = dto.VehicleModel,
                ManufacturingYear = dto.ManufacturingYear,
                Financier = dto.Financier,
                Capacity = dto.Capacity,                
                AuthorizedNamePan = dto.AuthorizedNamePan,
                AuthorizedNumberPan = dto.AuthorizedNumberPan,
                ExpiryDatePan = dto.ExpiryDatePan,
                ReminderDayPan = dto.ReminderDayPan,
                AuthorizedNameRc = dto.AuthorizedNameRc,
                ExpiryDateRc = dto.ExpiryDateRc,
                ReminderDayRc = dto.ReminderDayRc,
                RcNumber = dto.RcNumber,
                AuthorizedNameAlt = dto.AuthorizedNameAlt,
                AuthorizedNumberAlt = dto.AuthorizedNumberAlt,                
                ExpiryDateAlt = dto.ExpiryDateAlt,
                ReminderDayAlt = dto.ReminderDayAlt,                
                FileUploadPan = await SaveFile(dto.FileUploadPan),
                FileUploadRc = await SaveFile(dto.FileUploadRc),
                FileUploadAlt = await SaveFile(dto.FileUploadAlt),
                IsActive = true,
            };

            return await _vehicleRepository.AddVehicleAsync(entity);
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var uploadPath = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", "vehicle");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/vehicle/" + fileName;
        }
        public async Task<PaginatedResult<AddVehicleDto>> GetVehicleListAsync(VehiclePaginationFilterDto filter)
        {
            return await _vehicleRepository.GetVehicleListAsync(filter);
        }
        public async Task<bool?> ToggleStatusAsync(int id)
        {
            var user = await _vehicleRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            user.IsActive = !user.IsActive; // ✅ Directly toggle
            await _vehicleRepository.UpdateAsync(user);
            return user.IsActive;
        }
        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _vehicleRepository.GetVehicleByIdAsync(id);
        }
        public async Task<bool> IsVehicleNumberExistsForOtherAsync(int? id, string vehicleNumber)
        {
            return await _vehicleRepository.IsVehicleNumberExistsForOtherAsync(id, vehicleNumber);
        }
        public async Task<bool> UpdateVehicleAsync(AddVehicleDto dto)
        {
            var entity = await _vehicleRepository.GetVehicleByIdAsync(dto.Id);
            if (entity == null)
                return false;

            // Update properties
            entity.VehicleNumber = dto.VehicleNumber;
            entity.OwnerName = dto.OwnerName;
            entity.ChasisNumber = dto.ChasisNumber;
            entity.EngineNumber = dto.EngineNumber;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.Maker = dto.Maker;
            entity.VehicleModel = dto.VehicleModel;
            entity.ManufacturingYear = dto.ManufacturingYear;
            entity.Financier = dto.Financier;
            entity.Capacity = dto.Capacity;
            entity.IsActive = dto.IsActive ?? true;

            // PAN Authorization
            entity.AuthorizedNamePan = dto.AuthorizedNamePan;
            entity.AuthorizedNumberPan = dto.AuthorizedNumberPan;         
            entity.ExpiryDatePan = dto.ExpiryDatePan;
            entity.ReminderDayPan = dto.ReminderDayPan;

            // RC Authorization
            entity.AuthorizedNameRc = dto.AuthorizedNameRc;
            entity.ExpiryDateRc = dto.ExpiryDateRc;
            entity.ReminderDayRc = dto.ReminderDayRc;
            entity.RcNumber = dto.RcNumber;

            // ALT Authorization
            entity.AuthorizedNameAlt = dto.AuthorizedNameAlt;
            entity.AuthorizedNumberAlt = dto.AuthorizedNumberAlt;
            entity.ExpiryDateAlt = dto.ExpiryDateAlt;
            entity.ReminderDayAlt = dto.ReminderDayAlt;

            // Handle file uploads conditionally
            // PAN
            if (dto.FileUploadPan != null)
                entity.FileUploadPan = await SaveFile(dto.FileUploadPan);
            else if (!string.IsNullOrEmpty(dto.ExistingFileUploadPan))
                entity.FileUploadPan = dto.ExistingFileUploadPan;

            // RC
            if (dto.FileUploadRc != null)
                entity.FileUploadRc = await SaveFile(dto.FileUploadRc);
            else if (!string.IsNullOrEmpty(dto.ExistingFileUploadRc))
                entity.FileUploadRc = dto.ExistingFileUploadRc;

            // ALT
            if (dto.FileUploadAlt != null)
                entity.FileUploadAlt = await SaveFile(dto.FileUploadAlt);
            else if (!string.IsNullOrEmpty(dto.ExistingFileUploadAlt))
                entity.FileUploadAlt = dto.ExistingFileUploadAlt;
            return await _vehicleRepository.UpdateVehicleAsync(entity);
        }
    }
}