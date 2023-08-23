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

        public void Update(Almacen entity)
        {
            var almacenDb = _db.Almacenes.FirstOrDefault(x => x.Id == entity.Id);
            if (almacenDb != null)
            {
                almacenDb.Nombre = entity.Nombre;
                almacenDb.Descripcion = entity.Descripcion;
                almacenDb.Estado = entity.Estado;

                _db.SaveChanges();
            }
        }
    }
}
