using Logic.Students;
using Logic.Students.Models;

namespace Logic.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Course GetByName(string name);
    }
}