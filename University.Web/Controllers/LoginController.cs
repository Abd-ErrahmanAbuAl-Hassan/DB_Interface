using University.Web.Models;
using Microsoft.AspNetCore.Mvc;
using University.Entities.Repositories;

namespace University.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", model);
            }
            if (model.UserType == "Student")
            {
                var student = _unitOfWork.student.GetFirstorDefault(x => x.Ssn == model.SSN && x.Email == model.Email);
                if (student != null)
                {
                    return RedirectToAction("Index", "Student", new { studentId = student.StudentId });
                }
            }
            else if (model.UserType == "Staff")
            {
                int id = Convert.ToInt32(model.Email);
                var staff = _unitOfWork.staff.GetFirstorDefault(s => s.SSN == model.SSN && s.EmployeeId == id);

                if (staff != null)
                {
                    return RedirectToAction("Index", "Staff", new { staffId = staff.EmployeeId });
                }
            }
            else if (model.UserType == "System User")
            {
                int id = Convert.ToInt32(model.Email);
                var staff = _unitOfWork.staff.GetFirstorDefault(s => s.SSN == model.SSN && s.EmployeeId == id);

                if (staff != null)
                {
                    return RedirectToAction("Index", "SystemUser", new { staffId = staff.EmployeeId });
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return RedirectToAction("Index", model);
        }
    }
}
