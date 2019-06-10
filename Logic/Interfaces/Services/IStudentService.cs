using System.Collections.Generic;
using Logic.Students;

namespace Logic.Interfaces.Services
{
    public interface IStudentService
    {
        Student GetById(long id);
        IReadOnlyList<Student> GetList(string enrolledIn, int? numberOfCourses);
        void Save();
        void Add(Student student);
        void Delete(Student student);
    }
}