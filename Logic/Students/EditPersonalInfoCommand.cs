namespace Logic.Students
{
    public sealed class EditPersonalInfoCommand
    {
        public long Id { get; set; }    
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class EditPersonalInfoCommandHandler
    {
        public void Handle(EditPersonalInfoCommand command)
        {
            //var student = _studentService.GetById(id);
            //if (student == null)
            //    return Error($"No student found for Id {id}");
            //
            //student.Name = dto.Name;
            //student.Email = dto.Email;
            //
            //_studentService.Save();
        }
    }
}
