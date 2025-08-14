using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class UserTable
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string MemberId { get; set; } = null!;

    public string? BranchName { get; set; }

    public string? Name { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailId { get; set; }

    public string? Password { get; set; }

    public string? CurrentAddress { get; set; }

    public string? CurrentCity { get; set; }

    public string? CurrentDistrict { get; set; }

    public string? CurrentState { get; set; }

    public string? CurrentPincode { get; set; }

    public string? PermanentAddress { get; set; }

    public string? PermanentCity { get; set; }

    public string? PermanentDistrict { get; set; }

    public string? PermanentState { get; set; }

    public string? PermanentPincode { get; set; }

    public string? AccountHolderName { get; set; }

    public string? AccountNumber { get; set; }

    public string? Ifsccode { get; set; }

    public string? BankName { get; set; }

    public string? AadharFront { get; set; }

    public string? AadharBack { get; set; }

    public string? PanCard { get; set; }

    public string? Photo { get; set; }

    public string? Passbook { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
