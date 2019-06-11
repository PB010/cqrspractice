using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Interfaces.Services;
using Logic.Students.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Students.Handlers
{
    public class GetStudentListHandler : IQueryHandler<GetStudentListQuery, List<StudentDto>>
    {
        private readonly IStudentService _studentService;

        public GetStudentListHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public List<StudentDto> Handle(GetStudentListQuery query)
        {
            return _studentService.GetList(query.EnrolledIn, query.NumberOfCourses)
                .Select(StudentDto.ConvertToDto).ToList();

        }
    }   
}
