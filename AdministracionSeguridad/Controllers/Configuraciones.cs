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
    
    public partial class Configuraciones
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> UsuarioID { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
