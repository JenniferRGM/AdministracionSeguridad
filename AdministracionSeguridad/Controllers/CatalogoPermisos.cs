//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdministracionSeguridad.Controllers
{
    using System;
    using System.Collections.Generic;
    
    public partial class CatalogoPermisos
    {
        public int PermisoID { get; set; }
        public string DescripcionPermiso { get; set; }
    
        public virtual CatalogoPermisos CatalogoPermisos1 { get; set; }
        public virtual CatalogoPermisos CatalogoPermisos2 { get; set; }
    }
}
