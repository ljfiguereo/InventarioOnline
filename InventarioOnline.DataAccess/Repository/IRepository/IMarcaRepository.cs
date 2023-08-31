using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IMarcaRepository : IRepository<Marca>
    {
        void Update(Marca marca);
    }
}
