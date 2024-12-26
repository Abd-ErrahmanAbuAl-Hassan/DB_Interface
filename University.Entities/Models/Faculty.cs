using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Entities.Models;

public partial class Faculty
{
    [Column("Faculty_ID")]

    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employing> Employings { get; set; } = new List<Employing>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
