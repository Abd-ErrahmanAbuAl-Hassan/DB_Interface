using Microsoft.AspNetCore.Mvc;
using University.Entities.Models;
using University.Entities.Repositories;
using University.Web.Models;
using University.DataAccess;
using System.Linq;


namespace University.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(int studentId)
        {
            var student = _unitOfWork.student.GetFirstorDefault(s => s.StudentId == studentId, "Enrollments.Course,Department,Faculty");
            // int Did = student.DepartmentId;
            // var major = _unitOfWork.department.GetFirstorDefault(x => x.DepartmentId == Did);

            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = studentId;
            var studentViewModel = new StudentDashboardViewModel
            {
                StudentID = student.StudentId,
                SSN = student.Ssn,
                FullName = student.FirstName + " " + student.LastName,
                Gender = student.Gender,
                BirthOfDate = student.BirthOfDate,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address ?? " no add",
                Major = student.Department.DepartmentName,
                EnrollYear = student.EnrollYear,
                FacultyName = student.Faculty.FacultyName ?? "FFE"

            };

            return View(studentViewModel);
        }
        [HttpGet]
        public IActionResult Courses(int studentId)
        {
            var student = _unitOfWork.student.GetFirstorDefault(s => s.StudentId == studentId, "Enrollments.Course,Department");
            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = studentId;
            var studentViewModel = new StudentDashboardViewModel
            {
                StudentID = student.StudentId,
                FullName = student.FirstName + " " + student.LastName,
                Major = student.Department.DepartmentName,
                Enrollments = student.Enrollments.Select(e => new EnrollmentViewModel
                {
                    CourseId = e.Course.CourseId,
                    Grade = e.Grade,
                    Degree = e.Degree
                }).ToList(),
                courses = student.Enrollments.Select(e => new CoursesViewModel
                {
                    CourseId = e.Course.CourseId,
                    CourseName = e.Course.CourseName ?? "no name",
                    Credits = e.Course.Credits,
                    LapHours = e.Course.LapHours

                }).ToList()

            };
            return View(studentViewModel);
        }

        [HttpGet]
        public IActionResult Enroll(int studentId)
        {
            var student = _unitOfWork.student.GetFirstorDefault(s => s.StudentId == studentId, "Enrollments.Course");
            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = studentId;

            var availableCourses = _unitOfWork.course.GetAll()
                .Where(c => !student.Enrollments.Any(e => e.CourseId == c.CourseId))
                .Select(c => new CoursesViewModel
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName ?? "no name",
                    Credits = c.Credits,
                    Mark = c.Mark,
                    LapHours = c.LapHours
                }).ToList();

            var viewModel = new EnrollmentViewModel
            {
                StudentId = studentId,
                AvailableCourses = availableCourses
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EnrollPOST(int studentId, string courseId)
        {
            var student = _unitOfWork.student.GetFirstorDefault(s => s.StudentId == studentId);
            var course = _unitOfWork.course.GetFirstorDefault(c => c.CourseId == courseId);

            if (student == null || course == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = studentId;

            if (student.Enrollments.Any(e => e.CourseId == courseId))
            {
                ModelState.AddModelError("", "You are already enrolled in this course.");
                return RedirectToAction("Index", new { studentId });
            }
          
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "_",
                Degree = 0
            };

            _unitOfWork.enrollment.Add(enrollment);
            _unitOfWork.Complete();

            TempData["SuccessMessage"] = "You have successfully enrolled in the course!";
            return RedirectToAction("Enroll", new { studentId });

        }

        [HttpGet]
        public IActionResult Drop(int studentId)
        {
            var student = _unitOfWork.student.GetFirstorDefault(s => s.StudentId == studentId, "Enrollments.Course");
            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = studentId;
            var enrolledCourses = _unitOfWork.course.GetAll()
                .Where(c => student.Enrollments.Any(e => e.CourseId == c.CourseId))
                .Select(c => new CoursesViewModel
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    Credits = c.Credits,
                    Mark = c.Mark,
                    LapHours = c.LapHours
                }).ToList();

            var viewModel = new EnrollmentViewModel
            {
                StudentId = studentId,
                EnrolledCourses = enrolledCourses
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult DropPOST(int studentId, string courseId)
        {
            var student = _unitOfWork.student.GetFirstorDefault(s => s.StudentId == studentId);
            var course = _unitOfWork.course.GetFirstorDefault(c => c.CourseId == courseId);
            ViewData["StudentID"] = studentId;
            if (student == null || course == null)
            {
                return NotFound();
            }


            var enrollment = _unitOfWork.enrollment.GetFirstorDefault(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                ModelState.AddModelError("", "You are not enrolled in this course.");
                return RedirectToAction("Drop", new { studentId });
            }


            _unitOfWork.enrollment.Remove(enrollment);
            _unitOfWork.Complete();

            TempData["SuccessMessage"] = "You have successfully dropped the course!";
            return RedirectToAction("Drop", new { studentId });
        }


        
    }
}
