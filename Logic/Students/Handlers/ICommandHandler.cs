using CSharpFunctionalExtensions;
using Logic.Students.Commands;

namespace Logic.Students.Handlers
{
    public interface ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}