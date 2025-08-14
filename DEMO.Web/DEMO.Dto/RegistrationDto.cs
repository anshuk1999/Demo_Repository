using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto
{
    public class RegistrationDto
    {
        public int Id { get; set; }

        // Dropdown selections
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        // Employee info
        public string? EmployeeName { get; set; }
        public int EmployeeAge { get; set; }
        public string? EmployeeEmail { get; set; }
        public string? EmployeeMobile { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public decimal EmployeeSalary { get; set; }

        //// Dropdown lists
        //public List<CategoryPageDto> Categories { get; set; }
        //public List<SubCategoryPageDto> SubCategories { get; set; }
    }

}
