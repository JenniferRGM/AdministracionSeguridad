using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministracionSeguridad.Models
{
    public class Telefono
    {
        public int TelefonoID { get; set; }
        public int UsuarioID { get; set; }
        public string CodigoPais { get; set; }
        public string NumeroTelefono { get; set; }
    }
}