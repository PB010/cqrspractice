using Logic.Students;
using Logic.Students.Models;

namespace Logic.Interfaces.Services
{
    public interface ICourseService
    {
        Course GetByName(string name);
    }
}