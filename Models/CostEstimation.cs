using System;
using System.Collections.Generic;

namespace eisync_api.Models;

public partial class CostEstimation
{
    public string Id { get; set; } = null!;

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? CurrencyType { get; set; }

    public double? EstimatedCost { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<EnergyRate> EnergyRates { get; } = new List<EnergyRate>();

    public virtual User? User { get; set; }

    public virtual ICollection<Appliance> Appliances { get; } = new List<Appliance>();
}
