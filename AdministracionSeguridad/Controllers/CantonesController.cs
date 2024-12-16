
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace AdministracionSeguridad.Controllers
{
    public class CantonesController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Cantones
        public ActionResult Index(int? provinciaID)
        {
            // Carga las provincias para el filtro
            CargarProvincias(provinciaID);

            // Obtiene todos los cantones o filtrar por provincia si se selecciona una
            var cantones = db.Cantones.Include(c => c.Provincias);
            if (provinciaID.HasValue)
            {
                cantones = cantones.Where(c => c.ProvinciaID == provinciaID);
            }

            return View(cantones.ToList());
        }

        // GET: Cantones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cantones cantones = db.Cantones.Find(id);
            if (cantones == null)
            {
                return HttpNotFound();
            }

            return View(cantones);
        }

        // GET: Cantones/Create
        public ActionResult Create()
        {
            CargarProvincias(); // Carga provincias para el dropdown
            return View();
        }

        // POST: Cantones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CantonID,NombreCanton,ProvinciaID")] Cantones cantones)
        {
            if (ModelState.IsValid)
            {
                db.Cantones.Add(cantones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si hay un error, vuelve a cargar provincias
            CargarProvincias(cantones.ProvinciaID);
            return View(cantones);
        }

        // GET: Cantones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cantones cantones = db.Cantones.Find(id);
            if (cantones == null)
            {
                return HttpNotFound();
            }

            CargarProvincias(cantones.ProvinciaID);
            return View(cantones);
        }

        // POST: Cantones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CantonID,NombreCanton,ProvinciaID")] Cantones cantones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cantones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            CargarProvincias(cantones.ProvinciaID);
            return View(cantones);
        }

        // GET: Cantones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cantones cantones = db.Cantones.Find(id);
            if (cantones == null)
            {
                return HttpNotFound();
            }

            return View(cantones);
        }

        // POST: Cantones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cantones cantones = db.Cantones.Find(id);
            db.Cantones.Remove(cantones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Método auxiliar para cargar provincias en un SelectList
        private void CargarProvincias(int? provinciaSeleccionada = null)
        {
            ViewBag.ProvinciaID = new SelectList(db.Provincias, "ProvinciaID", "NombreProvincia", provinciaSeleccionada);
        }

        // Método para obtener cantones por provincia (usado con AJAX)
        public JsonResult ObtenerCantonesPorProvincia(int provinciaID)
        {
            var cantones = db.Cantones
                .Where(c => c.ProvinciaID == provinciaID)
                .Select(c => new { c.CantonID, c.NombreCanton })
                .ToList();

            return Json(cantones, JsonRequestBehavior.AllowGet);
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

