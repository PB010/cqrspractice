using Api.Dtos;
using Logic.Interfaces.Services;
using Logic.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;


        public StudentController(ICourseService courseService,
            IStudentService studentService)
        {
            _courseService = courseService;
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            var students = _studentService.GetList(enrolled, number)
                .Select(StudentDto.ConvertToDto);
            
            return Ok(students);
        }

        

        [HttpPost]
        public IActionResult Create([FromBody] StudentDto dto)
        {
            var student = new Student(dto.Name, dto.Email);

            if (dto.Course1 != null && dto.Course1Grade != null)
            {
                var course = _courseService.GetByName(dto.Course1);
                student.Enroll(course, Enum.Parse<Grade>(dto.Course1Grade));
            }

            if (dto.Course2 != null && dto.Course2Grade != null)
            {
                var course = _courseService.GetByName(dto.Course2);
                student.Enroll(course, Enum.Parse<Grade>(dto.Course2Grade));
            }

            _studentService.Add(student);
            _studentService.Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            _studentService.Delete(student);
           _studentService.Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] StudentDto dto)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            student.Name = dto.Name;
            student.Email = dto.Email;

            var firstEnrollment = student.FirstEnrollment;
            var secondEnrollment = student.SecondEnrollment;

            if (HasEnrollmentChanged(dto.Course1, dto.Course1Grade, firstEnrollment))
            {
                if (string.IsNullOrWhiteSpace(dto.Course1)) // Student disenrolls
                {
                    if (string.IsNullOrWhiteSpace(dto.Course1DisenrollmentComment))
                        return Error("Disenrollment comment is required");

                    var enrollment = firstEnrollment;
                    student.RemoveEnrollment(enrollment);
                    student.AddDisenrollmentComment(enrollment, dto.Course1DisenrollmentComment);
                }

                if (string.IsNullOrWhiteSpace(dto.Course1Grade))
                    return Error("Grade is required");

                var course = _courseService.GetByName(dto.Course1);

                if (firstEnrollment == null)
                {
                    // Student enrolls
                    student.Enroll(course, Enum.Parse<Grade>(dto.Course1Grade));
                }
                else
                {
                    // Student transfers
                    firstEnrollment.Update(course, Enum.Parse<Grade>(dto.Course1Grade));
                }
            }

            if (HasEnrollmentChanged(dto.Course2, dto.Course2Grade, secondEnrollment))
            {
                if (string.IsNullOrWhiteSpace(dto.Course2)) // Student disenrolls
                {
                    if (string.IsNullOrWhiteSpace(dto.Course2DisenrollmentComment))
                        return Error("Disenrollment comment is required");

                    var enrollment = secondEnrollment;
                    student.RemoveEnrollment(enrollment);
                    student.AddDisenrollmentComment(enrollment, dto.Course2DisenrollmentComment);
                }

                if (string.IsNullOrWhiteSpace(dto.Course2Grade))
                    return Error("Grade is required");

                var course = _courseService.GetByName(dto.Course2);

                if (secondEnrollment == null)
                {
                    // Student enrolls
                    student.Enroll(course, Enum.Parse<Grade>(dto.Course2Grade));
                }
                else
                {
                    // Student transfers
                    secondEnrollment.Update(course, Enum.Parse<Grade>(dto.Course2Grade));
                }
            }

            _studentService.Save();
           
            return Ok();
        }

        private bool HasEnrollmentChanged(string newCourseName, string newGrade, Enrollment enrollment)
        {
            if (string.IsNullOrWhiteSpace(newCourseName) && enrollment == null)
                return false;

            if (string.IsNullOrWhiteSpace(newCourseName) || enrollment == null)
                return true;

            return newCourseName != enrollment.Course.Name || newGrade != enrollment.Grade.ToString();
        }
    }
}
