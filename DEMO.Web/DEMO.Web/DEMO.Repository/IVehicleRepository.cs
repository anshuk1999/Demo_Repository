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
    public interface IVehicleRepository
    {
        Task<bool> AddVehicleAsync(Vehicle entity);
        Task<PaginatedResult<AddVehicleDto>> GetVehicleListAsync(VehiclePaginationFilterDto filter);
        Task<Vehicle> GetByIdAsync(int id);
        Task UpdateAsync(Vehicle user);
        Task<Vehicle> GetVehicleByIdAsync(int? id);
        Task<bool> IsVehicleNumberExistsForOtherAsync(int? id, string vehicleNumber);
        Task<bool> UpdateVehicleAsync(Vehicle entity);
    }
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddVehicleAsync(Vehicle entity)
        {
            await _context.Vehicles.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<PaginatedResult<AddVehicleDto>> GetVehicleListAsync(VehiclePaginationFilterDto filter)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                string search = filter.SearchTerm.ToLower();
                query = query.Where(v =>
                    v.VehicleNumber.ToLower().Contains(search) ||
                    v.OwnerName.ToLower().Contains(search) ||
                    v.EngineNumber.ToLower().Contains(search) ||
                    v.ChasisNumber.ToLower().Contains(search));
            }

            if (filter.FromDate.HasValue)
                query = query.Where(v => v.RegistrationDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(v => v.RegistrationDate <= filter.ToDate.Value);

            int totalCount = await query.CountAsync();

            switch (filter.SortColumn?.ToLower())
            {
                case "vehiclenumber":
                    query = filter.SortDirection == "asc" ? query.OrderBy(x => x.VehicleNumber) : query.OrderByDescending(x => x.VehicleNumber);
                    break;
                case "ownername":
                    query = filter.SortDirection == "asc" ? query.OrderBy(x => x.OwnerName) : query.OrderByDescending(x => x.OwnerName);
                    break;
                default:
                    query = query.OrderByDescending(x => x.Id);
                    break;
            }

            var data = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(v => new AddVehicleDto
                {
                    Id = v.Id,
                    VehicleNumber = v.VehicleNumber,
                    OwnerName = v.OwnerName,
                    ChasisNumber = v.ChasisNumber,
                    EngineNumber = v.EngineNumber,
                    RegistrationDate = v.RegistrationDate,
                    Maker = v.Maker,
                    VehicleModel = v.VehicleModel,
                    ManufacturingYear = v.ManufacturingYear,
                    Financier = v.Financier,
                    Capacity = v.Capacity,
                    IsActive = v.IsActive ?? true
                })
                .ToListAsync();

            return new PaginatedResult<AddVehicleDto>(data, totalCount);
        }
        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();
        //.FirstOrDefaultAsync(u => u.Id == id && u.RoleId == 2);
        }
        public async Task UpdateAsync(Vehicle user)
        {
            _context.Vehicles.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<Vehicle> GetVehicleByIdAsync(int? id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsVehicleNumberExistsForOtherAsync(int? id, string vehicleNumber)
        {
            return await _context.Vehicles.AnyAsync(x => x.VehicleNumber == vehicleNumber && x.Id != id);
        }
        public async Task<bool> UpdateVehicleAsync(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
