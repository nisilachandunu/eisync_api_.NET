using System;
using System.Collections.Generic;

namespace eisync_api.Models;

public partial class ApplianceGoal
{
    public string GoalId { get; set; } = null!;

    public string ApplianceId { get; set; } = null!;

    public int? OnHours { get; set; }

    public int? OnTime { get; set; }

    public int? OffTime { get; set; }

    public virtual Appliance Appliance { get; set; } = null!;

    public virtual Goal Goal { get; set; } = null!;
}
