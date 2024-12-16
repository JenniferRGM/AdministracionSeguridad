using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministracionSeguridad.Models
{
    public class Historial
    {
        public int ID { get; set; }
        public string UsuarioID { get; set; }
        public string Descripcion { get; set; }
        public string Modulo { get; set; }
        public string TipoEvento { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}