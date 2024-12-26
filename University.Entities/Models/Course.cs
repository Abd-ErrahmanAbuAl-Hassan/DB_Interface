using System;
using System.Collections.Generic;

namespace University.Entities.Models;

public partial class Course
{
    public string CourseId { get; set; } = null!;

    public string? CourseName { get; set; }

    public int Credits { get; set; }

    public string? Description { get; set; }

    public string Mark { get; set; } = null!;

    public string LapHours { get; set; } = null!;

    public virtual ICollection<DepartmentCourse> DepartmentCourses { get; set; } = new List<DepartmentCourse>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Teaching> Teaching { get; set; } = new List<Teaching>();
    
    public  ICollection<Staff> Staff = new List<Staff>();

}
