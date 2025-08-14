using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DEMO.Dto
{
    public class AddVehicleDto
    {
        public int Id { get; set; }

        public string? VehicleNumber { get; set; }

        //[Required]
        public string? OwnerName { get; set; }

        public string? ChasisNumber { get; set; }

        public string? EngineNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string? Maker { get; set; }

        public string? VehicleModel { get; set; }

        public DateTime? ManufacturingYear { get; set; }

        public string? Financier { get; set; }

        public int? Capacity { get; set; }

        public bool? IsActive { get; set; }

        public string? AuthorizedNamePan { get; set; }

        public string? AuthorizedNumberPan { get; set; }

        public IFormFile? FileUploadPan { get; set; }

        public DateTime? ExpiryDatePan { get; set; }

        public int? ReminderDayPan { get; set; }

        public string? AuthorizedNameRc { get; set; }

        public string? RcNumber { get; set; }

        public IFormFile? FileUploadRc { get; set; }

        public DateTime? ExpiryDateRc { get; set; }

        public int? ReminderDayRc { get; set; }

        public string? AuthorizedNameAlt { get; set; }

        public string? AuthorizedNumberAlt { get; set; }

        public IFormFile? FileUploadAlt { get; set; }

        public DateTime? ExpiryDateAlt { get; set; }

        public int? ReminderDayAlt { get; set; }
        //This field is use for edit case
        public string? ExistingFileUploadPan { get; set; }
        public string? ExistingFileUploadRc { get; set; }
        public string? ExistingFileUploadAlt { get; set; }
    }
    public class VehiclePaginationFilterDto
    {
        public string? SearchTerm { get; set; }
        public string SortColumn { get; set; } = "CreatedAt";
        public string SortDirection { get; set; } = "desc"; // or "asc"
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
    }
}
