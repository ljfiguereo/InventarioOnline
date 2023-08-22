using System.ComponentModel.DataAnnotations;

namespace InventarioOnline.Models.Inventario
{
    public class Almacen
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        [MaxLength(50,ErrorMessage ="El nombre debe tener un minimo de 50 caracteres")]
        public string Nombre { get; set; }
        [MaxLength(200,ErrorMessage ="La descripcion debe tener un minimo de 200 caracteres")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage ="El campo Estado es requerido")]
        public bool Estado { get; set; }
    }
}
