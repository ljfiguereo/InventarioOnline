using InventarioOnline.Models.Inventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventarioOnline.DataAccess.Configuracion
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ParentId).IsRequired(false);
            builder.Property(x => x.NumeroSerie).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Costo).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();
            builder.Property(x => x.MarcaId).IsRequired();
            builder.Property(x => x.ImagenUrl).IsRequired(false);
            builder.Property(x => x.FechaCreacion).IsRequired();
            builder.Property(x => x.Estado).IsRequired();

            // Relaciones
            builder.HasOne(x => x.Marca).WithMany().HasForeignKey(x => x.MarcaId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x=> x.Categoria).WithMany().HasForeignKey(x=>x.CategoriaId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Parent).WithMany().HasForeignKey(x=> x.ParentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
