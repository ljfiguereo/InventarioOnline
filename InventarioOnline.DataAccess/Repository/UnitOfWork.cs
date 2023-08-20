using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioOnline.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IAlmacenRepository Almacen { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Almacen = new AlmacenRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
