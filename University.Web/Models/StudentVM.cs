using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Entities.Models;

namespace University.Web.Models
{
    public class StudentVM
    {
        public Student Student { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> FacultyList { get; set; }
    }
}
