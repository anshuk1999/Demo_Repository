using AutoMapper;
using DEMO.Data.Entities;
using DEMO.Dto;

namespace DEMO.Web.Areas.Admin.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity -> DTO
            CreateMap<Vehicle, AddVehicleDto>()
                .ForMember(dest => dest.ExistingFileUploadPan, opt => opt.MapFrom(src => src.FileUploadPan))
                .ForMember(dest => dest.ExistingFileUploadRc, opt => opt.MapFrom(src => src.FileUploadRc))
                .ForMember(dest => dest.ExistingFileUploadAlt, opt => opt.MapFrom(src => src.FileUploadAlt))
                .ForMember(dest => dest.FileUploadPan, opt => opt.Ignore())
                .ForMember(dest => dest.FileUploadRc, opt => opt.Ignore())
                .ForMember(dest => dest.FileUploadAlt, opt => opt.Ignore());

            // DTO -> Entity
            CreateMap<AddVehicleDto, Vehicle>()
                .ForMember(dest => dest.FileUploadPan, opt => opt.Ignore())
                .ForMember(dest => dest.FileUploadRc, opt => opt.Ignore())
                .ForMember(dest => dest.FileUploadAlt, opt => opt.Ignore());

            // LoanCategory mappings
            CreateMap<LoanCategory, LoanCategoryDto>()
                .ForMember(dest => dest.ExistingLogoPath, opt => opt.MapFrom(src => src.LogoPath))
                .ForMember(dest => dest.LogoFile, opt => opt.Ignore());

            // DTO -> Entity
            CreateMap<LoanCategoryDto, LoanCategory>()
                .ForMember(dest => dest.LogoPath, opt => opt.Ignore()); // We'll handle it manually in service
        }
    }
}
