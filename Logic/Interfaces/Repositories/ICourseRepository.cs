using Logic.Students;

namespace Logic.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Course GetByName(string name);
    }
}