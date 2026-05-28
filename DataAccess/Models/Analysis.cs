using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Analysis
{
    public int AnalysisId { get; set; }

    public int ChildId { get; set; }

    public int? TotalSessions { get; set; }

    public int? CompletedSessions { get; set; }

    public int? TotalPracticeTime { get; set; }

    public double? AverageScore { get; set; }

    public string? ProgressLevel { get; set; }

    public string? Strengths { get; set; }

    public string? Weaknesses { get; set; }

    public string? Recommendation { get; set; }

    public DateTime? LastAnalysisAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Children Child { get; set; } = null!;
}
