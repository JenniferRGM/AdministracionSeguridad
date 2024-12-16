using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdministracionSeguridad.Models;

namespace AdministracionSeguridad.Controllers
{
    public class CatalogoPermisosController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: CatalogoPermisos
        public ActionResult Index()
        {
            var catalogoPermisos = db.CatalogoPermisos.ToList(); // Cambia a CatalogoPermiso
            return View(catalogoPermisos);
        }

        // GET: CatalogoPermisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoPermisos catalogoPermisos = db.CatalogoPermisos.Find(id); // Cambia a CatalogoPermiso
            if (catalogoPermisos == null)
            {
                return HttpNotFound();
            }
            return View(catalogoPermisos);
        }

        // GET: CatalogoPermisos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatalogoPermisos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermisoID,DescripcionPermiso")] CatalogoPermisos catalogoPermisos)
        {
            if (ModelState.IsValid)
            {
                db.CatalogoPermisos.Add(catalogoPermisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catalogoPermisos);
        }


        // GET: CatalogoPermisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoPermisos catalogoPermisos = db.CatalogoPermisos.Find(id); // Cambia a CatalogoPermiso
            if (catalogoPermisos == null)
            {
                return HttpNotFound();
            }
            return View(catalogoPermisos);
        }

        // POST: CatalogoPermisos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermisoID,DescripcionPermiso")] CatalogoPermiso catalogoPermiso) // Cambia a CatalogoPermiso
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogoPermiso).State = EntityState.Modified; // Cambia a CatalogoPermiso
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catalogoPermiso);
        }

        // GET: CatalogoPermisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoPermisos catalogoPermiso = db.CatalogoPermisos.Find(id); // Cambia a CatalogoPermiso
            if (catalogoPermiso == null)
            {
                return HttpNotFound();
            }
            return View(catalogoPermiso);
        }

        // POST: CatalogoPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CatalogoPermisos catalogoPermiso = db.CatalogoPermisos.Find(id); // Cambia a CatalogoPermiso
            db.CatalogoPermisos.Remove(catalogoPermiso); // Cambia a CatalogoPermiso
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


