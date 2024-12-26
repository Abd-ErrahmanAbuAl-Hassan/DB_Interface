using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Models;
using University.Entities.Repositories;

namespace University.DataAccess.Implementation
{
    public class CourseRepository : GenericRepository<Course>, ICourse
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Course course)
        {
            var courseInDb = _context.Courses.FirstOrDefault(x => x.CourseId == course.CourseId);
            if (courseInDb != null)
            {
                courseInDb.CourseId = course.CourseId;
                courseInDb.CourseName = course.CourseName;
                courseInDb.Credits = course.Credits;
                courseInDb.Description = course.Description;
                courseInDb.Mark = course.Mark;
                courseInDb.LapHours = course.LapHours;


            }
        }
    }
}
