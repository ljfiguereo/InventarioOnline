using InventarioOnline.Models.Inventario;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        void Update(Categoria categoria);
    }
}
