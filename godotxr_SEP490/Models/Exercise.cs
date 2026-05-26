using System;
using System.Collections.Generic;

namespace godotxr_SEP490.Models;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public int LessonId { get; set; }

    public int TypeId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public string? Instruction { get; set; }

    public string? DifficultyLevel { get; set; }

    public string? TargetSkill { get; set; }

    public string? Language { get; set; }

    public string? VisualPromptUrl { get; set; }

    public string? AudioPromptUrl { get; set; }

    public string? ContentHtml { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ExerciseType Type { get; set; } = null!;
}
