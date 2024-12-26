using System;
using System.Collections.Generic;

namespace University.Entities.Models;

public partial class DepartmentCourse
{
    public int DepartmentId { get; set; }

    public string CourseId { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string Semester { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;
}
