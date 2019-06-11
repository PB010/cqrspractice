using CSharpFunctionalExtensions;
using Logic.Interfaces.Services;
using Logic.Students.Commands;

namespace Logic.Students.Handlers
{
    public class EditPersonalInfoCommandHandler : ICommandHandler<EditPersonalInfoCommand>
    {
        private readonly IStudentService _studentService;

        public EditPersonalInfoCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public Result Handle(EditPersonalInfoCommand command)
        {
            var student = _studentService.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");
            
            student.Name = command.Name;
            student.Email = command.Email;
            
            _studentService.Save();

            return Result.Ok();
        }
    }
}