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
    public class DatosController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Datos
        public ActionResult Index()
        {
            var datos = db.Datos.Include(d => d.Categorias).Include(d => d.Usuarios).Include(d => d.Usuarios1);
            return View(datos.ToList());
        }

        // GET: Datos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos datos = db.Datos.Find(id);
            if (datos == null)
            {
                return HttpNotFound();
            }
            return View(datos);
        }

        // GET: Datos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre");
            return View();
        }

        // POST: Datos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UsuarioID,Descripcion,FechaCreacion,Modulo,TipoEvento,CategoriaID,FechaActualizacion")] Datos datos)
        {
            if (ModelState.IsValid)
            {
                db.Datos.Add(datos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", datos.CategoriaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", datos.UsuarioID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", datos.UsuarioID);
            return View(datos);
        }

        // GET: Datos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos datos = db.Datos.Find(id);
            if (datos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", datos.CategoriaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", datos.UsuarioID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", datos.UsuarioID);
            return View(datos);
        }

        // POST: Datos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UsuarioID,Descripcion,FechaCreacion,Modulo,TipoEvento,CategoriaID,FechaActualizacion")] Datos datos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", datos.CategoriaID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", datos.UsuarioID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombre", datos.UsuarioID);
            return View(datos);
        }

        // GET: Datos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datos datos = db.Datos.Find(id);
            if (datos == null)
            {
                return HttpNotFound();
            }
            return View(datos);
        }

        // POST: Datos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Datos datos = db.Datos.Find(id);
            db.Datos.Remove(datos);
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
