using System;
using System.Collections.Generic;

namespace godotxr_SEP490.Models;

public partial class Children
{
    public int ChildId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? LearningLevel { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Analysis> Analyses { get; set; } = new List<Analysis>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual User User { get; set; } = null!;
}
