using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Employee name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters are allowed in Name")]
        public string EmployeeName { get; set; } = null!;
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int EmployeeAge { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.com$", ErrorMessage = "Email must be valid and end with .com")]
        public string EmployeeEmail { get; set; } = null!;

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public string EmployeeMobile { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string District { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
        [Range(0, 1000000, ErrorMessage = "Salary must be between 0 and 1,000,000")]
        public decimal EmployeeSalary { get; set; }

        //// Dropdown lists
        //public List<CategoryPageDto> Categories { get; set; }
        //public List<SubCategoryPageDto> SubCategories { get; set; }
    }

}
