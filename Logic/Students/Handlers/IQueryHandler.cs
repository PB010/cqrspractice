using Logic.Students.Queries;

namespace Logic.Students.Handlers
{
    public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
