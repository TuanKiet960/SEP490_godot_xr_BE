using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int ChildId { get; set; }

    public int ExerciseId { get; set; }

    public int? AttemptNumber { get; set; }

    public string? CompletionStatus { get; set; }

    public double? Score { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int? DurationSeconds { get; set; }

    public string? AudioResultUrl { get; set; }

    public string? DisplayDataUrl { get; set; }

    public string? InteractionLog { get; set; }

    public string? FeedbackText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Children Child { get; set; } = null!;

    public virtual Exercise Exercise { get; set; } = null!;
}
