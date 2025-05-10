using System.Linq.Expressions;

namespace WL_Consultings_TestePratico.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> GetAllAsync(params string[] includes);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<IQueryable<T>> FindlAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}

