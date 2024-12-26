using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DataAccess
{
    [PrimaryKey(nameof(StudentId))]
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StaffId { get; set; }
        public string? Grade { get; set; }
        public int Degree { get; set; }
        public string CourseId { get; set; }
    }
}
