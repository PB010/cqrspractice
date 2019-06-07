using System.Linq;

namespace Logic.Utils
{
    public class UnitOfWork
    {
        private readonly StudentsDbContext _context;
        private readonly ITransaction _transaction;
        private bool _isAlive = true;

        public UnitOfWork(StudentsDbContext context)
        {
            _context = context;
        }

        

        internal T Get<T>(long id)
            where T : class
        {
            return _context.Get<T>(id);
        }

        internal void SaveOrUpdate<T>(T entity)
        {
            _context.Save(entity);
        }

        internal void Delete<T>(T entity)
        {
            _context.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _context.Query<T>();
        }

        public ISQLQuery CreateSQLQuery(string q)
        {
            return _context.CreateSQLQuery(q);
        }
    }
}
