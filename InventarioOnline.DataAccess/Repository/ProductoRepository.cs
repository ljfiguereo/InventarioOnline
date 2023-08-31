using InventarioOnline.DataAccess.Data;
using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarioOnline.DataAccess.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAllDropownList(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(x => x.Estado).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }
            if (obj == "Marca")
            {
                return _db.Marcas.Where(x => x.Estado).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }
            if (obj == "Producto")
            {
                return _db.Productos.Where(x => x.Estado).Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }

            return null;
        }

        public void Update(Producto entity)
        {
            var entityDb = _db.Productos.FirstOrDefault(x => x.Id == entity.Id);
            if (entityDb != null)
            {
                if (entity.ImagenUrl != null) entityDb.ImagenUrl = entity.ImagenUrl;
                
                entityDb.ParentId = entity.ParentId;
                entityDb.NumeroSerie = entity.NumeroSerie;
                entityDb.Nombre = entity.Nombre;
                entityDb.Descripcion = entity.Descripcion;
                entityDb.Costo = entity.Costo;
                entityDb.Precio = entity.Precio;
                entityDb.MarcaId = entity.MarcaId;
                entityDb.CategoriaId = entity.CategoriaId;
                entityDb.Estado = entity.Estado;

                _db.SaveChanges();
            }
        }
    }
}
