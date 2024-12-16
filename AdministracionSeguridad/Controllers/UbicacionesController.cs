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
    public class UbicacionesController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Ubicaciones
        public ActionResult Index()
        {
            var ubicaciones = db.Ubicaciones.Include(u => u.Cantones).Include(u => u.Distritos).Include(u => u.Provincias).Include(u => u.Usuarios);
            return View(ubicaciones.ToList());
        }

        // GET: Ubicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return HttpNotFound();
            }
            return View(ubicaciones);
        }

        // GET: Ubicaciones/Create
        public ActionResult Create()
        {
            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton");
            ViewBag.DistritoID = new SelectList(db.Distritos, "DistritoID", "NombreDistrito");
            ViewBag.ProvinciaID = new SelectList(db.Provincias, "ProvinciaID", "NombreProvincia");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre");
            return View();
        }

        // POST: Ubicaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UbicacionID,UsuarioID,ProvinciaID,CantonID,DistritoID,OtrasSenias")] Ubicaciones ubicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Ubicaciones.Add(ubicaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton", ubicaciones.CantonID);
            ViewBag.DistritoID = new SelectList(db.Distritos, "DistritoID", "NombreDistrito", ubicaciones.DistritoID);
            ViewBag.ProvinciaID = new SelectList(db.Provincias, "ProvinciaID", "NombreProvincia", ubicaciones.ProvinciaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", ubicaciones.UsuarioID);
            return View(ubicaciones);
        }

        // GET: Ubicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton", ubicaciones.CantonID);
            ViewBag.DistritoID = new SelectList(db.Distritos, "DistritoID", "NombreDistrito", ubicaciones.DistritoID);
            ViewBag.ProvinciaID = new SelectList(db.Provincias, "ProvinciaID", "NombreProvincia", ubicaciones.ProvinciaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", ubicaciones.UsuarioID);
            return View(ubicaciones);
        }

        // POST: Ubicaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UbicacionID,UsuarioID,ProvinciaID,CantonID,DistritoID,OtrasSenias")] Ubicaciones ubicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton", ubicaciones.CantonID);
            ViewBag.DistritoID = new SelectList(db.Distritos, "DistritoID", "NombreDistrito", ubicaciones.DistritoID);
            ViewBag.ProvinciaID = new SelectList(db.Provincias, "ProvinciaID", "NombreProvincia", ubicaciones.ProvinciaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", ubicaciones.UsuarioID);
            return View(ubicaciones);
        }

        // GET: Ubicaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return HttpNotFound();
            }
            return View(ubicaciones);
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            db.Ubicaciones.Remove(ubicaciones);
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
