using CSharpFunctionalExtensions;
using Logic.Interfaces.Services;
using Logic.Students.Commands;
using Logic.Students.Models;
using System;

namespace Logic.Students.Handlers
{

    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public RegisterCommandHandler(IStudentService studentService, ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }
        public Result Handle(RegisterCommand command)
        {
            var student = new Student(command.Name, command.Email);

            if (command.Course1 != null && command.Course1Grade != null)
            {
                var course = _courseService.GetByName(command.Course1);
                student.Enroll(course, Enum.Parse<Grade>(command.Course1Grade));
            }

            if (command.Course2 != null && command.Course2Grade != null)
            {
                var course = _courseService.GetByName(command.Course2);
                student.Enroll(course, Enum.Parse<Grade>(command.Course2Grade));
            }

            _studentService.Add(student);
            _studentService.Save();

            return Result.Ok();
        }
    }
}
