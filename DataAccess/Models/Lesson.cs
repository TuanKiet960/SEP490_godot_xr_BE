using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int ProgramId { get; set; }

    public string LessonName { get; set; } = null!;

    public int? LessonOrder { get; set; }

    public string? Description { get; set; }

    public string? TargetSkill { get; set; }

    public int? EstimatedDuration { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual Program Program { get; set; } = null!;
}
