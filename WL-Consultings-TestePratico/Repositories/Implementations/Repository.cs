using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WL_Consultings_TestePratico.Data;
using WL_Consultings_TestePratico.Repositories.Interfaces;

namespace WL_Consultings_TestePratico.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PostgreDbContext _context;

        public Repository(PostgreDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public virtual T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public virtual T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

        public virtual async Task<IQueryable<T>> FindlAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _context.Set<T>().Where(predicate).AsNoTracking().AsQueryable();
            query = ApplyIncludes(query, includes);

            return query;
        }

        public virtual async Task<IQueryable<T>> GetAllAsync(params string[] includes)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();
            query = ApplyIncludes(query, includes);

            return query;
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            query = ApplyIncludes(query, includes);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        protected IQueryable<T> ApplyIncludes(IQueryable<T> query, params string[] includes)
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }

}
