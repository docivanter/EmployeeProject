using System.Linq.Expressions;

namespace EmployeeProject.DataAccess.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);
        Task<T> GetById(Expression<Func<T, bool>> expression);
        IQueryable<T> GetQueryable<TItem>();
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(T obj);
    }
}
