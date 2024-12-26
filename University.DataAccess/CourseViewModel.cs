using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DataAccess
{
    [PrimaryKey(nameof(CourseId))]
    public class CourseViewModel
    {
        public string FacultyName { get; set; }
        public string DepartmentName { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
    }
}
