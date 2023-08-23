using InventarioOnline.Models.Inventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventarioOnline.DataAccess.Configuracion
{
    public class PruebaConfiguracion : IEntityTypeConfiguration<Prueba>
    {
        public void Configure(EntityTypeBuilder<Prueba> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Descripcion).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
