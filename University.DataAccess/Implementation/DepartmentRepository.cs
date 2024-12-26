using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartment
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Department department)
        {
            var departmentInDb = _context.Departments.FirstOrDefault(x => x.DepartmentId == department.DepartmentId);
            if (departmentInDb != null)
            {
                departmentInDb.DepartmentName = department.DepartmentName;
                departmentInDb.Building = department.Building;
                departmentInDb.Description = department.Description;

            }
        }
    }
}
