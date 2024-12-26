using System;
using System.Collections.Generic;

namespace University.Entities.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int FacultyId { get; set; }

    public string Ssn { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthOfDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? Address { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly EnrollYear { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Faculty Faculty { get; set; } = null!;
    public int DepartmentId { get; set; }

    public Department Department { get; set; }
}
