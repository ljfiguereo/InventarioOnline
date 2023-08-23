using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioOnline.Models.Inventario
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El Nombre es requerido")]
        [MaxLength(50,ErrorMessage = "La Marca debe tener un minimo de 50 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(200, ErrorMessage = "La Descripcion debe tener un minimo de 200 caracteres")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
    }
}
