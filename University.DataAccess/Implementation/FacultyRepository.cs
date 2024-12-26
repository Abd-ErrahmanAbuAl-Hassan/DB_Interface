using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class FacultyRepository : GenericRepository<Faculty>, IFaculty
    {
        private readonly ApplicationDbContext _context;
        public FacultyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Faculty faculty)
        {
            var facultyInDb = _context.Faculties.FirstOrDefault(x => x.FacultyId == faculty.FacultyId);
            if (facultyInDb != null)
            {
                facultyInDb.FacultyName = faculty.FacultyName;
                facultyInDb.Location = faculty.Location;
                facultyInDb.Description = faculty.Description;
                facultyInDb.Email = faculty.Email;
                facultyInDb.Phone = faculty.Phone;
                facultyInDb.Type = faculty.Type;
            }
        }
    }
}
