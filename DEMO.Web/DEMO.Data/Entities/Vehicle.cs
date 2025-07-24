using System;
using System.Collections.Generic;

namespace DEMO.Data.Entities;

public partial class Vehicle
{
    public int Id { get; set; }

    public string? VehicleNumber { get; set; }

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

    public string? FileUploadPan { get; set; }

    public DateTime? ExpiryDatePan { get; set; }

    public int? ReminderDayPan { get; set; }

    public string? AuthorizedNameRc { get; set; }

    public string? FileUploadRc { get; set; }

    public DateTime? ExpiryDateRc { get; set; }

    public int? ReminderDayRc { get; set; }

    public string? RcNumber { get; set; }

    public string? AuthorizedNameAlt { get; set; }

    public string? AuthorizedNumberAlt { get; set; }

    public string? FileUploadAlt { get; set; }

    public DateTime? ExpiryDateAlt { get; set; }

    public int? ReminderDayAlt { get; set; }
}
