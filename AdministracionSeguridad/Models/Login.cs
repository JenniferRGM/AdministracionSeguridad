using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministracionSeguridad.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(50)]
        public string Usuario { get; set; }

        [Required]
        [StringLength(20)]
        public string Clave { get; set; }

        [ForeignKey("Rol")]
        public int RolID { get; set; }

        public virtual Rol Rol { get; set; }
    }
}