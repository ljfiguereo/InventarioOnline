using InventarioOnline.Models.Inventario;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IProductoRepository : IRepository<Producto>
    {
        void Update(Producto producto);

        IEnumerable<SelectListItem> GetAllDropownList(string obj, int? id = null);
    }
}
