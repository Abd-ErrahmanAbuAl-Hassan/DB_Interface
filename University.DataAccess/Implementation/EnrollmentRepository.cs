using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollment
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Enrollment enrollment)
        {
            var enrollmentInDb = _context.Enrollments.FirstOrDefault(x => x.StudentId == enrollment.StudentId 
                                                                       && x.CourseId == enrollment.CourseId);

            if (enrollmentInDb == null)
            {
                throw new Exception("Enrollment not found.");
            }

            enrollmentInDb.Grade = enrollment.Grade;
            enrollmentInDb.Degree = enrollment.Degree;
        }
    }
}
