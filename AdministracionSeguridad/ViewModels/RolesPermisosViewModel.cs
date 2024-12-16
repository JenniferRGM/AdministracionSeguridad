using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministracionSeguridad.ViewModels
{
    public class RolesPermisosViewModel
    {
        public string NombreMenu { get; set; }
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoEliminacion { get; set; }
    }
}