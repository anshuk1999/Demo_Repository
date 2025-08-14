using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class AdminLogin
{
    public int Id { get; set; }

    public string FirmName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
