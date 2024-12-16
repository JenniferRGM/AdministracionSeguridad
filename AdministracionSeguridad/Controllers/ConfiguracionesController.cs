using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AdministracionSeguridad.Controllers
{
    public class ConfiguracionesController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Configuraciones
        public ActionResult Index()
        {
            var configuraciones = db.Configuraciones.Include(c => c.Usuarios);
            return View(configuraciones.ToList());
        }

        // GET: Configuraciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuraciones configuraciones = db.Configuraciones.Find(id);
            if (configuraciones == null)
            {
                return HttpNotFound();
            }
            return View(configuraciones);
        }

        // GET: Configuraciones/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre");
            return View();
        }

        // POST: Configuraciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Valor,FechaCreacion,FechaActualizacion,UsuarioID")] Configuraciones configuraciones)
        {
            if (ModelState.IsValid)
            {
                db.Configuraciones.Add(configuraciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", configuraciones.UsuarioID);
            return View(configuraciones);
        }

        // GET: Configuraciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuraciones configuraciones = db.Configuraciones.Find(id);
            if (configuraciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", configuraciones.UsuarioID);
            return View(configuraciones);
        }

        // POST: Configuraciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Valor,FechaCreacion,FechaActualizacion,UsuarioID")] Configuraciones configuraciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuraciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", configuraciones.UsuarioID);
            return View(configuraciones);
        }

        // GET: Configuraciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuraciones configuraciones = db.Configuraciones.Find(id);
            if (configuraciones == null)
            {
                return HttpNotFound();
            }
            return View(configuraciones);
        }

        // POST: Configuraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configuraciones configuraciones = db.Configuraciones.Find(id);
            db.Configuraciones.Remove(configuraciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
