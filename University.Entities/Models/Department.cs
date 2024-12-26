using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Entities.Models;

public partial class Department
{
    private List<Department> departments;

    [Column("Department_ID")]
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string Building { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<DepartmentCourse> DepartmentCourses { get; set; } = new List<DepartmentCourse>();

    public virtual ICollection<Employing> Employings { get; set; } = new List<Employing>();

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Staff> Staff { get; set; } = new List<Staff>();
    public ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();


}
