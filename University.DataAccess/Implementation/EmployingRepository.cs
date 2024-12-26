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
    public class EmployingRepository : GenericRepository<Employing>, IEmploying
    {
        private readonly ApplicationDbContext _context;
        public EmployingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Employing employing)
        {
            var employingInDb = _context.Employings.FirstOrDefault(x => x.EmployeeId == employing.EmployeeId);
            if (employingInDb != null)
            {
                employingInDb.EmployeeId = employing.EmployeeId;
                employingInDb.DepartmentId = employing.DepartmentId;
                employingInDb.FacultyId = employing.FacultyId;
                employingInDb.HireDate = employing.HireDate;
               
            }
        }
    }
}
