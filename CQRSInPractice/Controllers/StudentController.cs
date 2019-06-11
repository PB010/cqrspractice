using Logic.Dtos;
using Logic.Interfaces.Services;
using Logic.Students;
using Logic.Students.Commands;
using Logic.Students.Handlers;
using Logic.Students.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly Messages _messages;
        private readonly IMediator _mediator;


        public StudentController(ICourseService courseService,
            IStudentService studentService, Messages messages,
            IMediator mediator)
        {
            _courseService = courseService;
            _studentService = studentService;
            _messages = messages;
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<StudentDto> GetList(string enrolled, int? number)
        //{
        //    var result = await _mediator.Send(new GetStudentListQuery(enrolled, number));
        //    var handler = new GetStudentListHandler(_studentService);
        //    return Ok(handler.Handle(new GetStudentListQuery(enrolled, number)));
        //    //return Ok(_messages.Dispatch(new GetStudentListQuery(enrolled, number)));
        //}

        

        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            var handler = new RegisterCommandHandler(_studentService, _courseService);

            return Ok(handler.Handle(new RegisterCommand(
                dto.Name,
                dto.Email,
                dto.Course1,
                dto.Course1Grade,
                dto.Course2,
                dto.Course2Grade)));
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
            var command = new EditPersonalInfoCommand(id, dto.Name, dto.Email);

            var result = _messages.Dispatch(command);

            return result.IsSuccess ? Ok() : Error(result.Error);
        }
    }
}
