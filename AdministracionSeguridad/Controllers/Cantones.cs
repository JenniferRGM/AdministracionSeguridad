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
    
    public partial class Cantones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cantones()
        {
            this.Distritos = new HashSet<Distritos>();
            this.Ubicaciones = new HashSet<Ubicaciones>();
        }
    
        public int CantonID { get; set; }
        public string NombreCanton { get; set; }
        public int ProvinciaID { get; set; }
    
        public virtual Provincias Provincias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Distritos> Distritos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ubicaciones> Ubicaciones { get; set; }
    }
}