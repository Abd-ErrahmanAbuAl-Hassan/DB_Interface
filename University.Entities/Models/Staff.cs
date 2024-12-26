

namespace University.Entities.Models;

public partial class Staff
{
    public int EmployeeId { get; set; }
    public string SSN { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }

    public virtual ICollection<Employing> Employings { get; set; } = new List<Employing>();

    public virtual ICollection<Teaching> Teaching { get; set; } = new List<Teaching>();

    public  ICollection<Course> Courses = new List<Course>();
    public  ICollection<Department> Departments = new List<Department>();
    public  ICollection<Faculty> Faculties = new List<Faculty>();


}
