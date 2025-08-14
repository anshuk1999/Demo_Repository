using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto
{
    public class LoanCategoryDto
    {
        public int Id { get; set; }
        //[Required]
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public IFormFile? LogoFile { get; set; } // for uploading
        public string? ExistingLogoPath { get; set; } // to show existing image
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
    public class LoanCategoryPaginationFilterDto
    {
        public string? SearchTerm { get; set; }
        public string SortColumn { get; set; } = "CreatedAt";
        public string SortDirection { get; set; } = "desc"; // or "asc"
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CategoryName { get; set; }

    }
}
