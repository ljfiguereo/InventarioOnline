using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository
{
    public class AlmacenRepository : Repository<Almacen>, IAlmacenRepository
    {
        private readonly ApplicationDbContext _db;
        public AlmacenRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Almacen almacen)
        {
            var almacenDb = _db.Almacenes.FirstOrDefault(x => x.Id == almacen.Id);
            if (almacenDb != null)
            {
                almacenDb.Nombre = almacen.Nombre;
                almacenDb.Descripcion = almacen.Descripcion;
                almacenDb.Estado = almacen.Estado;

                _db.SaveChanges();
            }
        }
    }
}
