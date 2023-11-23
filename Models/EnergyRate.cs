using System;
using System.Collections.Generic;

namespace eisync_api.Models;

public partial class EnergyRate
{
    public string Id { get; set; } = null!;

    public double? FixedCharge { get; set; }

    public double? FromUnit { get; set; }

    public double? ToUnit { get; set; }

    public double? Charge { get; set; }

    public int? Order { get; set; }

    public string? CostEstimationId { get; set; }

    public virtual CostEstimation? CostEstimation { get; set; }
}
