using InventarioOnline.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IMarcaRepository : IRepository<Marca>
    {
        void Update(Marca marca);
    }
}
