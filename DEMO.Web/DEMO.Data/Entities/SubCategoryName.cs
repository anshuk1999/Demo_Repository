using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class SubCategoryName
{
    public int Id { get; set; }

    public string SubName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool Status { get; set; }

    public int CategoryId { get; set; }

    public virtual CategoryName Category { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
