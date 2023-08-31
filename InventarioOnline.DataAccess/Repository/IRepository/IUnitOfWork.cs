namespace InventarioOnline.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAlmacenRepository Almacen { get; }
        ICategoriaRepository Categoria { get; }
        IMarcaRepository Marca { get; }
        IPruebaRepository Prueba { get; }
        IProductoRepository Producto { get; }

        Task Save();
    }
}
