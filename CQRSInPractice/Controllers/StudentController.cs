using Api.Dtos;
using API.Dtos;
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
        public IActionResult Register([FromBody] NewStudentDto dto)
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
        public IActionResult Unregister(long id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            _studentService.Delete(student);
           _studentService.Save();

            return Ok();
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            var course = _courseService.GetByName(dto.Course);
            if (course == null)
                return Error($"Course is incorrect: '{dto.Course}'");

            var success = Enum.TryParse(dto.Grade, out Grade grade);
            if (!success)
                return Error($"Grade is incorrect: '{dto.Grade}'");

            student.Enroll(course, grade);
            _studentService.Save();

            return Ok();
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(long id, int enrollmentNumber,
            [FromBody] StudentTransferDto dto)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            var course = _courseService.GetByName(dto.Course);
            if (course == null)
                return Error($"Course is incorrect: '{dto.Course}'");

            var success = Enum.TryParse(dto.Grade, out Grade grade);
            if (!success)
                return Error($"Grade is incorrect: '{dto.Grade}'");

            var enrollment = student.GetEnrollment(enrollmentNumber);

            if (enrollment == null)
                return Error($"No enrollment found with number '{enrollmentNumber}'");

            enrollment.Update(course, grade);
            _studentService.Save();

            return Ok();
        }

        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(long id, int enrollmentNumber,
            [FromBody] StudentDisenrollmentDto dto)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            if (string.IsNullOrWhiteSpace(dto.Comment))
                return Error("Disenrollment comment is required");

            var enrollment = student.GetEnrollment(enrollmentNumber);

            if (enrollment == null)
                return Error($"No enrollment found with number '{enrollmentNumber}'");

            student.RemoveEnrollment(enrollment, dto.Comment);

            _studentService.Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto dto)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            student.Name = dto.Name;
            student.Email = dto.Email;

            _studentService.Save();

            return Ok();
        }
    }
}
