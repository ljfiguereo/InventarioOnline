using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventarioOnline.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = db.Set<T>();
        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity); // Insert Into Table
        }
        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id); // Select * From (Just By Id)
        }
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, 
            bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter); // Select * From Where ....
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',',StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(property); // Ejemplo "Categoria, Marca"
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public PagedList<T> GetAllPaged(Params param, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter); // Select * From Where ....
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property); // Ejemplo "Categoria, Marca"
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return PagedList<T>.ToPagedList(query, param.PageNumber, param.PageSize);
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> filter = null, 
            string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter); // Select * From Where ....
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property); // Ejemplo "Categoria, Marca"
                }
            }
            
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstAsync();
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
