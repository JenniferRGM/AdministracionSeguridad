using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministracionSeguridad.Models
{
    public class Dato
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string CategoriaNombre { get; set; }
        public int UsuarioID { get; set; }
        public string Modulo { get; set; }
        public string TipoEvento { get; set; }
        public int CategoriaID { get; set; }
    }
}