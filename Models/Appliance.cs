using System;
using System.Collections.Generic;

namespace eisync_api.Models;

public partial class Appliance
{
    public string Id { get; set; } = null!;

    public string? DeviceType { get; set; }

    public double? Power { get; set; }

    public double? Voltage { get; set; }

    public int? OnHours { get; set; }

    public string? DeviceModel { get; set; }

    public string? DeviceBrand { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<ApplianceGoal> ApplianceGoals { get; } = new List<ApplianceGoal>();

    public virtual User? User { get; set; }

    public virtual ICollection<CostEstimation> CostEstimations { get; } = new List<CostEstimation>();
}
