using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class CategoryName
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual ICollection<SubCategoryName> SubCategoryNames { get; set; } = new List<SubCategoryName>();
}
