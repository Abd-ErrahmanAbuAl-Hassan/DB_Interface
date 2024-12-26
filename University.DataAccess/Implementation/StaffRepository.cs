using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class StaffRepository : GenericRepository<Staff>, IStaff
    {
        private readonly ApplicationDbContext _context;
        public StaffRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Staff staff)
        {
            var staffInDb = _context.Staff.FirstOrDefault(x => x.EmployeeId == staff.EmployeeId);
            if (staffInDb != null)
            {
                staffInDb.FirstName = staff.FirstName;
                staffInDb.LastName = staff.LastName;
                staffInDb.Salary = staff.Salary;
                staffInDb.Position = staff.Position;

            }
        }

    }
}
