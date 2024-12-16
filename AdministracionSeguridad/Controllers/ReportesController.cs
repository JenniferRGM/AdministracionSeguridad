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
    public class ReportesController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Reportes
        public ActionResult Index()
        {
            var reportes = db.Reportes.Include(r => r.Usuarios);
            return View(reportes.ToList());
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportes reportes = db.Reportes.Find(id);
            if (reportes == null)
            {
                return HttpNotFound();
            }
            return View(reportes);
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre");
            return View();
        }

        // POST: Reportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,Fecha,UsuarioID")] Reportes reportes)
        {
            if (ModelState.IsValid)
            {
                db.Reportes.Add(reportes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", reportes.UsuarioID);
            return View(reportes);
        }

        // GET: Reportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportes reportes = db.Reportes.Find(id);
            if (reportes == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", reportes.UsuarioID);
            return View(reportes);
        }

        // POST: Reportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,Fecha,UsuarioID")] Reportes reportes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", reportes.UsuarioID);
            return View(reportes);
        }

        // GET: Reportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportes reportes = db.Reportes.Find(id);
            if (reportes == null)
            {
                return HttpNotFound();
            }
            return View(reportes);
        }

        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reportes reportes = db.Reportes.Find(id);
            db.Reportes.Remove(reportes);
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
