using Logic.Interfaces.Repositories;
using Logic.Students;
using System.Linq;
using Logic.Students.Models;

namespace Logic.Repositories
{
    public sealed class CourseRepository : ICourseRepository
    {
        private readonly StudentsDbContext _context;

        public CourseRepository(StudentsDbContext context)
        {
            _context = context;
        }

        public Course GetByName(string name)
        {
            return _context.Courses.Single(c => c.Name == name);
        }
    }
}