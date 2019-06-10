using Logic.Interfaces.Repositories;
using Logic.Students;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Repositories
{
    public sealed class StudentRepository : IStudentRepository
    {
        private readonly StudentsDbContext _context;

        public StudentRepository(StudentsDbContext context)
        {
            _context = context;
        }

        public Student GetById(long id)
        {
            return _context.Students.SingleOrDefault(s => s.Id == id);
        }

        public IReadOnlyList<Student> GetList(string enrolledIn, int? numberOfCourses)
        {
            var studentList = _context.Students.AsQueryable();
                
            if (!string.IsNullOrWhiteSpace(enrolledIn))
            {
                 studentList = _context.Students
                    .Where(s => s.Enrollments
                        .Any(e => e.Course.Name == enrolledIn));
            }

            var result = studentList.ToList();

            if (numberOfCourses != null)
            {
                result = result.Where(x => x.Enrollments.Count == numberOfCourses).ToList();
            }

            return result;
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            _context.Remove(student);
            _context.SaveChanges();
        }
    }
}
