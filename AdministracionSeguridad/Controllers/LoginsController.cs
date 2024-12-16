using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdministracionSeguridad.Models;

namespace AdministracionSeguridad.Controllers
{
    public class LoginsController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Página de inicio de sesión
        public ActionResult Login()
        {
            return View();
        }

        // POST: Autenticación de usuarios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(string usuario, string clave)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Usuario y contraseña son obligatorios.";
                return View("Login");
            }

            // Busca el usuario en la base de datos
            var login = db.Login.Include(l => l.Roles).FirstOrDefault(l => l.Usuario == usuario);

            if (login != null && login.Clave == clave)
            {
                // Configura sesión
                Session["UsuarioID"] = login.UsuarioID;
                Session["Usuario"] = login.Usuario;
                Session["Rol"] = login.Roles.NombreRol;

                // Obtiene y configura permisos
                var permisos = ObtenerPermisosPorRol(login.RolID);
                Session["Permisos"] = permisos;

                // Obtiene y configurar menús agrupados
                var menusAgrupados = ObtenerMenusAgrupados(login.RolID);
                Session["MenusAgrupados"] = menusAgrupados;

                // Redirige al Home
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View("Login");
        }

        // Método para obtener permisos por rol
        private Dictionary<string, Dictionary<string, bool>> ObtenerPermisosPorRol(int? rolID)
        {
            if (rolID == null)
            {
                return new Dictionary<string, Dictionary<string, bool>>();
            }

            var permisos = db.Roles_Permisos
                .Where(rp => rp.RolID == rolID)
                .Select(rp => new
                {
                    NombreMenu = rp.Menus.NombreMenu,
                    Lectura = rp.PermisoLectura,
                    Escritura = rp.PermisoEscritura,
                    Modificacion = rp.PermisoModificacion,
                    Eliminacion = rp.PermisoEliminacion
                })
                .ToList();

            return permisos.ToDictionary(
                p => p.NombreMenu,
                p => new Dictionary<string, bool>
                {
                    { "Lectura", p.Lectura },
                    { "Escritura", p.Escritura },
                    { "Modificacion", p.Modificacion },
                    { "Eliminacion", p.Eliminacion }
                });
        }

        // Método para obtener menús agrupados por rol
        private Dictionary<string, List<Menu>> ObtenerMenusAgrupados(int? rolID)
        {
            if (rolID == null)
            {
                return new Dictionary<string, List<Menu>>();
            }

            var menusAgrupados = db.Roles_Permisos
                .Where(rp => rp.RolID == rolID && rp.PermisoLectura)
                .Select(rp => new
                {
                    rp.Menus.GrupoMenu,
                    Menu = new Menu
                    {
                        NombreMenu = rp.Menus.NombreMenu,
                        URL = rp.Menus.URL
                    }
                })
                .GroupBy(m => m.GrupoMenu)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(m => m.Menu).ToList()
                );

            return menusAgrupados;
        }

        // GET: Logins
        public ActionResult Index()
        {
            var login = db.Login.Include(l => l.Roles);
            return View(login.ToList());
        }

        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Login login = db.Login.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol");
            return View();
        }

        // POST: Logins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioID,Usuario,Clave,RolID")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Login.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", login.RolID);
            return View(login);
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Login login = db.Login.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", login.RolID);
            return View(login);
        }

        // POST: Logins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,Usuario,Clave,RolID")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", login.RolID);
            return View(login);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Login login = db.Login.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Login.Find(id);
            db.Login.Remove(login);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // MÉTODO PARA CERRAR SESIÓN
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
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
