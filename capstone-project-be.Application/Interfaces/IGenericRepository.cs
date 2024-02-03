using System.Linq.Expressions;

namespace capstone_project_be.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> Find(Expression<Func<T,bool>> predicate);
    }
}
