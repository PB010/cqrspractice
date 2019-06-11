using Logic.Interfaces.Repositories;
using Logic.Interfaces.Services;
using Logic.Students;
using Logic.Students.Models;

namespace Logic.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _course;

        public CourseService(ICourseRepository course)
        {
            _course = course;
        }

        public Course GetByName(string name)
        {
            return _course.GetByName(name);
        }
    }
}
