using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Courselab.Data.Respositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CourselabDbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(CourselabDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            T entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity == null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? dbSet : dbSet.Where(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T> InsertAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public T Update(T entity)
        {
            return dbSet.Update(entity).Entity;
        }
    }
}
