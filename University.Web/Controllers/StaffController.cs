using University.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using University.Entities.Models;
using University.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using University.DataAccess;
using System.Diagnostics;

namespace University.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
       
        public StaffController(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public IActionResult Index(int staffId)
        {
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId, "Teaching.Course,Employings.Department,Employings.Faculty");


            if (staff == null)
            {
                return NotFound();
            }
            ViewData["StaffID"] = staffId;
            var staffViewModel = new StaffDashboardViewModel
            {
                staffId = staff.EmployeeId,
                StaffName = staff.FirstName + " " + staff.LastName,
                ssn = staff.SSN,
                salary = staff.Salary,
                position = staff.Position,
                


                Department = staff.Employings.Select(s => new DepartmentViewModel
                {

                    departmentId = s.DepartmentId,
                    departmentName = s.Department.DepartmentName ?? "no name",
                    FacultyName = s.Faculty.FacultyName ?? "no name",
                    FacultyAddress = s.Faculty.Location,
                    hireDate =s.HireDate
                }).ToList()

            };
            return View(staffViewModel);
        }

        [HttpGet]
        
        public async Task<IActionResult> Courses(int staffId)
        {
            ViewData["StaffID"] = staffId;
            var query = @"
                SELECT f.Faculty_Name AS FacultyName,d.Department_Name AS DepartmentName, 
                       c.Course_Id AS CourseId,c.Course_Name AS CourseName,dc.Level,dc.Semester 
                FROM Departments d
                JOIN DepartmentCourses dc ON d.Department_ID = dc.Department_id
                JOIN Courses c ON c.Course_ID = dc.Course_id
                JOIN Employing e ON d.Department_ID = e.Department_id
                JOIN Faculty f ON f.Faculty_ID = e.Faculty_id
                JOIN Staff s ON s.Employee_ID = e.Employee_id
                WHERE s.Employee_ID = @staffId"
            ;

            var courses = await _context.CourseViewModel
                .FromSqlRaw(query, new SqlParameter("@staffId", staffId))
                .ToListAsync();

            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> CoursesDetail(int staffId)
        {
            ViewData["StaffID"] = staffId;

            var query = @"
        SELECT c.Course_Name AS CourseName, s.Student_Id AS StudentId,s.First_Name + ' ' + s.Last_Name AS StudentName, 
               en.Grade, en.Degree , st.Employee_ID AS StaffId ,c.Course_ID AS CourseId
        FROM Students s
        JOIN Enrollments en ON en.Student_id = s.Student_ID
        JOIN Courses c ON c.Course_ID = en.Course_id
        JOIN Teachings t ON t.CourseId = c.Course_ID
        JOIN Staff st ON st.Employee_ID = t.StaffId
        WHERE st.Employee_ID = @staffId";

            var courseDetails = await _context.CourseDetailViewModel
                .FromSqlRaw(query, new SqlParameter("@staffId", staffId))
                .ToListAsync();

            
            var groupedCourses = courseDetails
                .GroupBy(cd => cd.CourseName)
                .Select(g => new CourseDetailViewModel
                {
                    CourseName = g.Key,
                    EnrolledStudents = g.Select(cd => new StudentViewModel
                    {
                        StudentId = cd.StudentId,
                        StudentName = cd.StudentName,
                        Grade = cd.Grade,
                        Degree = cd.Degree,
                        StaffId = cd.StaffId,
                        CourseId = cd.CourseId,
                       
                    }).ToList()
                }).ToList();

            return View(groupedCourses);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGrades(int staffId,List<StudentViewModel> students)
        {
         

            foreach (var student in students)
            {
                var enrollment = _unitOfWork.enrollment.GetFirstorDefault(
                    e => e.StudentId == student.StudentId && e.CourseId == student.CourseId);

                if (enrollment != null)
                {
                    enrollment.Grade = student.Grade;
                    enrollment.Degree = student.Degree;
                    _unitOfWork.enrollment.Update(enrollment);
                }
            }

            _unitOfWork.Complete();

            TempData["Message"] = "Grades updated successfully!";
            return RedirectToAction("CoursesDetail", new { staffId });
        }
        

    }
}


