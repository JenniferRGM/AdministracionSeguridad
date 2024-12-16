using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AdministracionSeguridad.Models;

namespace AdministracionSeguridad.Controllers
{
    
    public class HistorialController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();
        // GET: Historial
        public ActionResult Index()
        {
            var historial = db.Datos.Select(d => new Historial
            {
                ID = d.ID,
                UsuarioID = d.UsuarioID.ToString(),
                Descripcion = d.Descripcion,
                FechaCreacion = d.FechaCreacion,
                Modulo = d.Modulo,
                TipoEvento = d.TipoEvento
            })
                .ToList();
               
            return View(historial.ToList());
        }
    }
}