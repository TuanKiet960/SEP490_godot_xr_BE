using System;
using System.Collections.Generic;

namespace godotxr_SEP490.Models;

public partial class Classroom
{
    public int ClassId { get; set; }

    public int UserId { get; set; }

    public int ProgramId { get; set; }

    public string ClassName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Program Program { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
