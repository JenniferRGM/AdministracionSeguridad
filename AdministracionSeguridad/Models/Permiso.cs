using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministracionSeguridad.Models
{
    [Table("Permisos")]
    public class Permiso
    {
        [Key]
        public int PermisoID { get; set; }

        [Required]
        public string NombrePantalla { get; set; }
        [Required]
        public string TipoPermiso { get; set; } 

        [ForeignKey("Rol")]
        public int RolID { get; set; }

        public virtual Rol Rol { get; set; }
    }
}