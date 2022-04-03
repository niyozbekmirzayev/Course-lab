using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Courselab.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
    }
}
