using System.ComponentModel.DataAnnotations;

namespace InventarioOnline.Models.Inventario
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El Nombre es requerido")]
        [MaxLength(50,ErrorMessage = "El Nombre debe tener maximo 50 caracteres")]
        public string Nombre { get; set; }
        
        [MaxLength(200,ErrorMessage = "La descripcion debe tener maximo 200 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
    }
}
