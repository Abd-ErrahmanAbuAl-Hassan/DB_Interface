using System;
using System.Collections.Generic;

namespace University.Entities.Models;

public partial class Enrollment
{
    public int StudentId { get; set; }

    public string CourseId { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public int Degree { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
