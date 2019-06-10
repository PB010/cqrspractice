using Logic.Interfaces.Repositories;
using Logic.Interfaces.Services;
using Logic.Students;
using System.Collections.Generic;

namespace Logic.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _student;

        public StudentService(IStudentRepository student)
        {
            _student = student;
        }

        public Student GetById(long id)
        {
            return _student.GetById(id);
        }

        public IReadOnlyList<Student> GetList(string enrolledIn, int? numberOfCourses)
        {
            return _student.GetList(enrolledIn, numberOfCourses);
        }

        public void Save()
        {
            _student.Save();
        }

        public void Add(Student student)
        {
            _student.Add(student);
        }

        public void Delete(Student student)
        {
            _student.Delete(student);
        }
    }
}
