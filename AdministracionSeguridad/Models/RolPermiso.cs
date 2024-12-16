using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdministracionSeguridad.Models
{
    public class RolPermiso
    {
        public int RolID { get; set; }
        public int PermisoID { get; set; }
        public int MenuID { get; set; }
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoEliminacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        [ForeignKey("MenuID")]
        public virtual Menu Menus { get; set; }
    }
}