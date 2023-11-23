using System;
using System.Collections.Generic;

namespace eisync_api.Models;

public partial class Goal
{
    public string Id { get; set; } = null!;

    public string? GoalName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public double? GoalAmount { get; set; }

    public bool? IsActive { get; set; }

    public string? Status { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<ApplianceGoal> ApplianceGoals { get; } = new List<ApplianceGoal>();

    public virtual User? User { get; set; }
}
