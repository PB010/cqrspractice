using Logic.Students;
using System.Collections.Generic;
using Logic.Students.Models;

namespace Logic.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Student GetById(long id);
        IReadOnlyList<Student> GetList(string enrolledIn, int? numberOfCourses);
        void Add(Student student);
        void Save();
        void Delete(Student student);
    }
}