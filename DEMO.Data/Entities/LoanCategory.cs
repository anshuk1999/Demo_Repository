using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class LoanCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? LogoPath { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public string? SubCategoryName { get; set; }
}
