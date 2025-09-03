using DEMO.Dto;
using DEMO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Services
{
    public interface IAdminAuthService
    {
        Task<AdminLoginResponseDto?> AuthenticateAsync(AdminLoginDto dto);
    }

    public class AdminAuthService : IAdminAuthService
    {
        private readonly IAdminLoginRepository _repo;

        public AdminAuthService(IAdminLoginRepository repo)
        {
            _repo = repo;
        }

        public async Task<AdminLoginResponseDto?> AuthenticateAsync(AdminLoginDto dto)
        {
            var admin = await _repo.GetByEmailAsync(dto.EmailId);
            if (admin == null)
                return null;
            if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.Password))
                return null;

            return new AdminLoginResponseDto
            {
                Name = admin.EmailId,
                Token = Guid.NewGuid().ToString(),
                ExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
