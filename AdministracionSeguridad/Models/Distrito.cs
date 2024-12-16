using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministracionSeguridad.Models
{
    public class Distrito
    {
        public int DistritoID { get; set; }
        public string NombreDistrito { get; set; }
        public int CantonID { get; set; }
    }
}