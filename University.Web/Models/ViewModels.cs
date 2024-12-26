namespace University.Web.Models
{
   
    public class StudentDashboardViewModel
    {
        public int StudentID { get; set; }
        public string? SSN { get; set; }
        public string? Gender { get; set; }
        public DateOnly? BirthOfDate { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Major { get; set; }
        public string? FacultyName { get; set; }
        public double? GPA { get; set; }
        public string? Phone { get; set; }
        public DateOnly? EnrollYear { get; set; }
        public List<EnrollmentViewModel> Enrollments { get; set; } = new List<EnrollmentViewModel>();
        public List<CoursesViewModel> courses { get; set; } = new List<CoursesViewModel>();
    }

    public class EnrollmentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string? CourseId { get; set; }
        public string? Grade { get; set; }
        public int Degree { get; set; }
        public List<CoursesViewModel>? AvailableCourses { get; set; }
        public List<CoursesViewModel>? EnrolledCourses { get; set; }
    }
    public class TeachingViewModel
    {
        public int staffId { get; set; }
        public string? courseId { get; set; }
        public List<CoursesViewModel>? AvailableCourses { get; set; }
        public List<CoursesViewModel>? TeachingCourses { get; set; }
    }
    public class CoursesViewModel
    {
        public string? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int Credits { get; set; }
        public string? Mark { get; set; }
        public string? LapHours { get; set; }
        public string Level { get; set; }
        public string? FacultyName { get; set; }
        public string? DepartmentName { get; set; }
        public string Semester { get; set; }
        public List<DepartmentViewModel> Department { get; set; } = new List<DepartmentViewModel>();
        public string? Faculty { get; set; }
    }
    public class DepartmentViewModel
    {
        public int departmentId { get; set; }
        public string? departmentName { get; set; }
        public string? FacultyName { get; set; }
        public string Level { get; set; }
        public DateOnly hireDate { get; set; }
        public string FacultyAddress { get; set; }
        public string Semester { get; set; }

    }

    public class StaffDashboardViewModel
    {
        public int staffId { get; set; }
        public string? StaffName { get; set; }
        public string? position { get; set; }
        public string? ssn { get; set; }
        public string? facultyName { get; set; }
        public List<CoursesViewModel> Teaching { get; set; } = new List<CoursesViewModel>();
        public List<DepartmentViewModel> Department { get; set; } = new List<DepartmentViewModel>();
        public decimal salary { get; set; }
        public DateOnly hireDate { get; set; }
    }
}
