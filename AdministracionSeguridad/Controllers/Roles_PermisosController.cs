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
    public class Roles_PermisosController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Roles_Permisos
        public ActionResult Index()
        {
            var roles_Permisos = db.Roles_Permisos.Include(r => r.Menus).Include(r => r.Permisos).Include(r => r.Roles);
            return View(roles_Permisos.ToList());
        }

        // GET: Roles_Permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles_Permisos roles_Permisos = db.Roles_Permisos.Find(id);
            if (roles_Permisos == null)
            {
                return HttpNotFound();
            }
            return View(roles_Permisos);
        }

        // GET: Roles_Permisos/Create
        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "NombreMenu");
            ViewBag.PermisoID = new SelectList(db.Permisos, "PermisoID", "NombrePermiso");
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol");
            return View();
        }

        // POST: Roles_Permisos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RolID,PermisoID,MenuID,PermisoLectura,PermisoEscritura,PermisoModificacion,PermisoEliminacion,FechaCreacion,FechaActualizacion")] Roles_Permisos roles_Permisos)
        {
            if (ModelState.IsValid)
            {
                db.Roles_Permisos.Add(roles_Permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "NombreMenu", roles_Permisos.MenuID);
            ViewBag.PermisoID = new SelectList(db.Permisos, "PermisoID", "NombrePermiso", roles_Permisos.PermisoID);
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", roles_Permisos.RolID);
            return View(roles_Permisos);
        }

        // GET: Roles_Permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles_Permisos roles_Permisos = db.Roles_Permisos.Find(id);
            if (roles_Permisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "NombreMenu", roles_Permisos.MenuID);
            ViewBag.PermisoID = new SelectList(db.Permisos, "PermisoID", "NombrePermiso", roles_Permisos.PermisoID);
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", roles_Permisos.RolID);
            return View(roles_Permisos);
        }

        // POST: Roles_Permisos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RolID,PermisoID,MenuID,PermisoLectura,PermisoEscritura,PermisoModificacion,PermisoEliminacion,FechaCreacion,FechaActualizacion")] Roles_Permisos roles_Permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles_Permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "NombreMenu", roles_Permisos.MenuID);
            ViewBag.PermisoID = new SelectList(db.Permisos, "PermisoID", "NombrePermiso", roles_Permisos.PermisoID);
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", roles_Permisos.RolID);
            return View(roles_Permisos);
        }

        // GET: Roles_Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles_Permisos roles_Permisos = db.Roles_Permisos.Find(id);
            if (roles_Permisos == null)
            {
                return HttpNotFound();
            }
            return View(roles_Permisos);
        }

        // POST: Roles_Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles_Permisos roles_Permisos = db.Roles_Permisos.Find(id);
            db.Roles_Permisos.Remove(roles_Permisos);
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
