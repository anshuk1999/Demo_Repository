using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class Registration
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int EmployeeAge { get; set; }

    public string EmployeeEmail { get; set; } = null!;

    public string EmployeeMobile { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public decimal EmployeeSalary { get; set; }

    public virtual CategoryName Category { get; set; } = null!;

    public virtual SubCategoryName SubCategory { get; set; } = null!;
}