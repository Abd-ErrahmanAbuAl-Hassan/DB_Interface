using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DataAccess
{
    [PrimaryKey(nameof(CourseName))]
    public class CourseDetailViewModel
    {
        public string CourseName { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public string CourseId { get; set; }
        public string StudentName { get; set; }
        public string? Grade { get; set; }
        public int Degree { get; set; }
        public List<StudentViewModel> EnrolledStudents { get; set; } = new();
    }
}
