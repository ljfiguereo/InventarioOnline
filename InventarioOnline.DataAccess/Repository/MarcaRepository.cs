using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository
{
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        private readonly ApplicationDbContext _db;
        public MarcaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Marca entity)
        {
            var entityDb = _db.Marcas.FirstOrDefault(x => x.Id == entity.Id);
            if (entityDb != null)
            {
                entityDb.Nombre = entity.Nombre;
                entityDb.Descripcion = entity.Descripcion;
                entityDb.Estado = entity.Estado;

                _db.SaveChanges();
            }
        }
    }
}
