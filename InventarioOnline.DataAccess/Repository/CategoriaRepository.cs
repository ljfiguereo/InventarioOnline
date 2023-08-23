using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Categoria entity)
        {
            var entityDb = _db.Categorias.FirstOrDefault(x => x.Id == entity.Id);
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
