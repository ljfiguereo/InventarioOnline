using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IAlmacenRepository : IRepository<Almacen>
    {
        void Update(Almacen almacen);
    }
}
