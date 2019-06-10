using Logic.Students;

namespace Logic.Interfaces.Services
{
    public interface ICourseService
    {
        Course GetByName(string name);
    }
}