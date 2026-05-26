using System;
using System.Collections.Generic;

namespace godotxr_SEP490.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int ChildId { get; set; }

    public int ClassId { get; set; }

    public string? EnrollmentCode { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Children Child { get; set; } = null!;

    public virtual Classroom Class { get; set; } = null!;
}
