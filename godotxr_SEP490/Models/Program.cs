using System;
using System.Collections.Generic;

namespace godotxr_SEP490.Models;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public string? Description { get; set; }

    public int? TargetAgeFrom { get; set; }

    public int? TargetAgeTo { get; set; }

    public string? Language { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
