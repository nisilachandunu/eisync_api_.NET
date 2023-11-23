using System;
using System.Collections.Generic;

namespace eisync_api.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Country { get; set; }

    public string? LoginPassword { get; set; }

    public virtual ICollection<Appliance> Appliances { get; } = new List<Appliance>();

    public virtual ICollection<CostEstimation> CostEstimations { get; } = new List<CostEstimation>();

    public virtual ICollection<Goal> Goals { get; } = new List<Goal>();
}
