using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.DataAccess;
using University.Entities.Models;
using University.Entities.Repositories;
using University.Web.Models;

namespace University.Web.Controllers
{
    public class SystemUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private int _userId;
        public SystemUserController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public IActionResult Error(int staffId)
        {
            ViewData["StaffID"] = staffId;

            return View(staffId);
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
                    hireDate = s.HireDate
                }).ToList()

            };
            return View(staffViewModel);
        }
        [HttpGet]
        public IActionResult ViewAllStudents(int staffId)
        {
            ViewData["StaffID"] = staffId;
            _userId=staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s=>s.EmployeeId==staffId);
            
            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }

            var students = _unitOfWork.student.GetAll();
            return View(students);
        }
        [HttpGet]
        public IActionResult ViewAllStaff(int staffId)
        {
            ViewData["StaffID"] = staffId;
            _userId = staffId;
            var staffInDb = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staffInDb.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }

            var staffList = _unitOfWork.staff.GetAll(null,"Teaching.Course,Employings.Department,Employings.Faculty").ToList();

            if (staffList == null || !staffList.Any())
            {
                return NotFound();
            }

            var staffViewModels = staffList.Select(staff => new StaffDashboardViewModel
            {
                staffId = staff.EmployeeId,
                StaffName = $"{staff.FirstName} {staff.LastName}",
                ssn = staff.SSN,
                salary = staff.Salary,
                position = staff.Position,
                Department = staff.Employings.Select(emp => new DepartmentViewModel
                {
                    departmentId = emp.DepartmentId,
                    departmentName = emp.Department.DepartmentName ?? "No Name",
                    FacultyName = emp.Faculty.FacultyName ?? "No Faculty",
                    FacultyAddress = emp.Faculty.Location ?? "No Address",
                    hireDate = emp.HireDate
                }).ToList(),
                Teaching = staff.Teaching.Select(t => new CoursesViewModel
                {
                    CourseId = t.Course.CourseId,
                    CourseName = t.Course.CourseName ?? "No Name",
                    Credits = t.Course.Credits
                }).ToList()

            }).ToList();

            return View(staffViewModels);
        }
        [HttpGet]
        public IActionResult Edit(int staffId,int? studentId)
        {
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }
            if (studentId == null | studentId == 0)
                NotFound();
           
            var studentInDb = _unitOfWork.student.GetFirstorDefault(x => x.StudentId == studentId);
            return View(studentInDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int staffId,Student student)
        {
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }


            _unitOfWork.student.Update(student);
                _unitOfWork.Complete();
                TempData["Edit"] = "Student has Updated Successfuly";
                return RedirectToAction("ViewAllStudents", new { staffId = staffId });
          

        }
		[HttpGet]
		public IActionResult Remove(int staffId, int? studentId)
		{
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }
            if (studentId == null || studentId == 0)
				return NotFound();

			var studentInDb = _unitOfWork.student.GetFirstorDefault(x => x.StudentId == studentId);
			if (studentInDb == null)
			{
				return NotFound();
			}

			return View(studentInDb); 
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Remove(int staffId, int studentId)
		{
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }
            var studentInDb = _unitOfWork.student.GetFirstorDefault(x => x.StudentId == studentId);
			if (studentInDb == null)
			{
				return NotFound();
			}
          
            var enrollments = _unitOfWork.enrollment.GetAll(e => e.StudentId == studentId).ToList();
            _unitOfWork.enrollment.RemoveRange(enrollments);
			_unitOfWork.student.Remove(studentInDb);
			_unitOfWork.Complete();

			TempData["Delete"] = "Student has been deleted successfully.";
			return RedirectToAction("ViewAllStudents", new { staffId = staffId });
		}
        [HttpGet]
        public IActionResult AddNewStudent(int staffId)
        {
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }
            StudentVM studentVM = new()
            {
                Student = new Student(),
                DepartmentList = _unitOfWork.department.GetAll().ToList().Select(x => new SelectListItem
                {
                    Text=x.DepartmentName,
                    Value=x.DepartmentId.ToString()
                }),
                FacultyList = _unitOfWork.faculty.GetAll().ToList().Select(x => new SelectListItem
                {
                    Text = x.FacultyName,
                    Value = x.FacultyId.ToString()
                })
            };            
            return View(studentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewStudent(int staffId, Student student)
        {
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }
           
            _unitOfWork.student.Add(student);
            _unitOfWork.Complete();

            TempData["Success"] = "New student added successfully.";
            return RedirectToAction("ViewAllStudents", new { staffId = staffId });
            
        }
        [HttpGet]
        public IActionResult AddNewStaff(int staffId)
        {
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewStaff(int staffId, Staff Staff)
        {
            ViewData["StaffID"] = staffId;
            var staff = _unitOfWork.staff.GetFirstorDefault(s => s.EmployeeId == staffId);

            if (staff.Position != "Administrator")
            {
                return RedirectToAction("Error", new { staffId = staffId });
            }

            _unitOfWork.staff.Add(Staff);
            _unitOfWork.Complete();

            TempData["Success"] = "New staff member added successfully.";
            return RedirectToAction("ViewAllStaff", new { staffId = staffId });
         
        }

    }
}
