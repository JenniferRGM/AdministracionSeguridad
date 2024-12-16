using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministracionSeguridad.Models
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int RolID { get; set; }

        [Required]
        public string NombreRol { get; set; }
        public virtual ICollection<Permiso> Permisos { get; set; } = new HashSet<Permiso>();

        public virtual ICollection<Login> Logins { get; set; } = new HashSet<Login>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}