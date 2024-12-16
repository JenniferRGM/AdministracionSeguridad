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
    public class TelefonosController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Telefonos
        public ActionResult Index()
        {
            var telefonos = db.Telefonos.Include(t => t.Usuarios);
            return View(telefonos.ToList());
        }

        // GET: Telefonos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos telefonos = db.Telefonos.Find(id);
            if (telefonos == null)
            {
                return HttpNotFound();
            }
            return View(telefonos);
        }

        // GET: Telefonos/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre");
            return View();
        }

        // POST: Telefonos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TelefonoID,UsuarioID,CodigoPais,NumeroTelefono")] Telefonos telefonos)
        {
            if (ModelState.IsValid)
            {
                db.Telefonos.Add(telefonos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", telefonos.UsuarioID);
            return View(telefonos);
        }

        // GET: Telefonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos telefonos = db.Telefonos.Find(id);
            if (telefonos == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", telefonos.UsuarioID);
            return View(telefonos);
        }

        // POST: Telefonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TelefonoID,UsuarioID,CodigoPais,NumeroTelefono")] Telefonos telefonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", telefonos.UsuarioID);
            return View(telefonos);
        }

        // GET: Telefonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos telefonos = db.Telefonos.Find(id);
            if (telefonos == null)
            {
                return HttpNotFound();
            }
            return View(telefonos);
        }

        // POST: Telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefonos telefonos = db.Telefonos.Find(id);
            db.Telefonos.Remove(telefonos);
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
