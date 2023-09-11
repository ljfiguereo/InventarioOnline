﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioOnline.Models.Admin
{
    public class UsuarioApp:IdentityUser
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(80)]
        public string Nombres { get; set; }
        
        [Required(ErrorMessage = "Apellidos es requerido")]
        [MaxLength(80)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Direccion es requerida")]
        [MaxLength(80)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ciudad es requerida")]
        [MaxLength(60)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Pais es requerido")]
        [MaxLength(60)]
        public string Pais { get; set; }

        [NotMapped] // No la agrega a la tabla
        public string Role { get; set; }
    }
}
