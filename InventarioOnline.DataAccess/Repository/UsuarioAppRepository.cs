using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Admin;
using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository
{
    public class UsuarioAppRepository : Repository<UsuarioApp>, IUsuarioAppRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioAppRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
