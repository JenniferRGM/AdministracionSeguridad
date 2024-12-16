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
    public class DistritosController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Distritos
        public ActionResult Index()
        {
            var distritos = db.Distritos.Include(d => d.Cantones);
            return View(distritos.ToList());
        }

        // GET: Distritos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distritos distritos = db.Distritos.Find(id);
            if (distritos == null)
            {
                return HttpNotFound();
            }
            return View(distritos);
        }

        // GET: Distritos/Create
        public ActionResult Create()
        {
            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton");
            return View();
        }

        // POST: Distritos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistritoID,NombreDistrito,CantonID")] Distritos distritos)
        {
            if (ModelState.IsValid)
            {
                db.Distritos.Add(distritos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton", distritos.CantonID);
            return View(distritos);
        }

        // GET: Distritos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distritos distritos = db.Distritos.Find(id);
            if (distritos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton", distritos.CantonID);
            return View(distritos);
        }

        // POST: Distritos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DistritoID,NombreDistrito,CantonID")] Distritos distritos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distritos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CantonID = new SelectList(db.Cantones, "CantonID", "NombreCanton", distritos.CantonID);
            return View(distritos);
        }

        // GET: Distritos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distritos distritos = db.Distritos.Find(id);
            if (distritos == null)
            {
                return HttpNotFound();
            }
            return View(distritos);
        }

        // POST: Distritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Distritos distritos = db.Distritos.Find(id);
            db.Distritos.Remove(distritos);
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
