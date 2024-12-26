using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;
        public IStudent student { get; private set; }
        public ICourse course { get; private set; }
        public IStaff staff { get; private set; }
        public IFaculty faculty { get; private set; }
        public IEmploying employing { get; private set; }
        public IDepartment department { get; private set; }

        public IEnrollment enrollment { get; private set; }



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            student = new StudentRepository(context);
            course = new CourseRepository(context);
            faculty = new FacultyRepository(context);
            department = new DepartmentRepository(context);
            staff = new StaffRepository(context);
            enrollment = new EnrollmentRepository(context);
            employing = new EmployingRepository(context);

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
