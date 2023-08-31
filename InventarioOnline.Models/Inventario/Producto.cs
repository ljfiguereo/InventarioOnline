using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioOnline.Models.Inventario
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }
        public virtual Producto Parent { get; set; }

        [Required(ErrorMessage = "El Numero de Serie es requerido")]
        [MaxLength(50)]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Precio es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El Costo es requerido")]
        public decimal Costo { get; set; }

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "La Categoria es requerida")]
        public int CategoriaId { get; set; }
        
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "La Marca es requerida")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
    }
}
