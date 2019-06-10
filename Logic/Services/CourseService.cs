using Logic.Interfaces.Repositories;
using Logic.Interfaces.Services;
using Logic.Students;

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
