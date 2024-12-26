using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class StudentRepository : GenericRepository<Student>, IStudent
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Student student)
        {
            var studentInDb = _context.Students.FirstOrDefault(x => x.StudentId == student.StudentId);
            if (studentInDb != null)
            {
                studentInDb.Ssn = student.Ssn;
                studentInDb.Email = student.Email;
                studentInDb.FirstName = student.FirstName;
                studentInDb.LastName = student.LastName;
                studentInDb.Address = student.Address;
                studentInDb.BirthOfDate = student.BirthOfDate;
                studentInDb.Phone = student.Phone;
                studentInDb.Gender = student.Gender;
                studentInDb.EnrollYear = student.EnrollYear;
            }
        }
    }
}
