using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Entities.Models;

public partial class Employing
{
    [Column("Faculty_id")]
    public int FacultyId { get; set; }

    [Column("Department_id")]
    public int DepartmentId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Staff Employee { get; set; } = null!;

    public virtual Faculty Faculty { get; set; } = null!;
}
