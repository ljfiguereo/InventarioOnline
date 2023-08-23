using InventarioOnline.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAlmacenRepository Almacen { get; }
        ICategoriaRepository Categoria { get; }
        IMarcaRepository Marca { get; }
        Task Save();
    }
}
