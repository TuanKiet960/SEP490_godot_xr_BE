using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ExerciseType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
