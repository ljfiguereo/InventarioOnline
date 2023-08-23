using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository
{
    public class PruebaRepository : Repository<Prueba>, IPruebaRepository
    {
        private readonly ApplicationDbContext _db;
        public PruebaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Prueba entity)
        {
            var almacenDb = _db.Prueba.FirstOrDefault(x => x.Id == entity.Id);
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
