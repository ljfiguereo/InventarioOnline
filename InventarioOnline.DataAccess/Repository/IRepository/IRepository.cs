using System.Linq.Expressions;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             string includeProperties = null,
             bool isTracking = true
            );
        Task<T> GetFirst(
            Expression<Func<T, bool>> filter = null,
             string includeProperties = null,
             bool isTracking = true
            );
        Task  Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
