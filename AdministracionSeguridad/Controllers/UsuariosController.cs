using AdministracionSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdministracionSeguridad.Controllers
{
    public class UsuariosController : Controller
    {
        private AdminSeguridadEntities db = new AdminSeguridadEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Login).Include(u => u.Roles);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Login, "UsuarioID", "Usuario");
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioID,Nombre,Apellido1,Apellido2,Email,Clave,FechaCreacion,FechaActualizacion,RolID,UsuarioNombre,FotoPath")] Usuarios usuarios, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null && Foto.ContentLength > 0)
                {
                    // Valida la extensión del archivo
                    var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                    var fileExt = Path.GetExtension(Foto.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt.ToLower()))
                    {
                        ModelState.AddModelError("Foto", "Solo se permiten archivos de imagen (jpg, jpeg, png, gif).");
                        ViewBag.UsuarioID = new SelectList(db.Login, "UsuarioID", "Usuario", usuarios.UsuarioID);
                        ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuarios.RolID);
                        return View(usuarios); // Retorno en caso de error
                    }

                    // Guarda la foto en la carpeta "Fotos"
                    string fileName = Path.GetFileName(Foto.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Fotos"), fileName);
                    Foto.SaveAs(filePath);

                    // Guarda la ruta de la foto en la base de datos
                    usuarios.FotoPath = "/Fotos/" + fileName;
                }

                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }

            // Si el modelo no es válido, regresa a la vista con los datos actuales
            ViewBag.UsuarioID = new SelectList(db.Login, "UsuarioID", "Usuario", usuarios.UsuarioID);
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuarios.RolID);
            return View(usuarios); 
        }

        public void RegistrarUsuario(string usuario, string contraseña, int rolId)
        {
            var nuevoUsuario = new Login
            {
                Usuario = usuario,
                Clave = BCrypt.Net.BCrypt.HashPassword(contraseña), // Encripta la contraseña
                RolID = rolId
            };

            db.Login.Add(nuevoUsuario);
            db.SaveChanges();
        }



        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuarioEntidad = db.Usuarios.Find(id);
            if (usuarioEntidad == null)
            {
                return HttpNotFound();
            }

            // Mapea de Usuarios (EF) a Usuario (modelo manual)
            var usuario = new Usuario
            {
                UsuarioID = usuarioEntidad.UsuarioID,
                Nombre = usuarioEntidad.Nombre,
                Apellido1 = usuarioEntidad.Apellido1,
                Apellido2 = usuarioEntidad.Apellido2,
                Email = usuarioEntidad.Email,
                UsuarioNombre = usuarioEntidad.UsuarioNombre,
                Clave = usuarioEntidad.Clave,
                FechaCreacion = usuarioEntidad.FechaCreacion,
                FechaActualizacion = usuarioEntidad.FechaActualizacion,
                FotoPath = usuarioEntidad.FotoPath,
                RolID = usuarioEntidad.RolID
            };

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuario.RolID);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,Nombre,Apellido1,Apellido2,Email,UsuarioNombre,Clave,FechaCreacion,FechaActualizacion,FotoPath,RolID")] Usuario usuario, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                var usuarioEntidad = db.Usuarios.Find(usuario.UsuarioID);
                if (usuarioEntidad == null)
                {
                    return HttpNotFound();
                }

                // Mapea de Usuario (modelo manual) a Usuarios (EF)
                usuarioEntidad.Nombre = usuario.Nombre;
                usuarioEntidad.Apellido1 = usuario.Apellido1;
                usuarioEntidad.Apellido2 = usuario.Apellido2;
                usuarioEntidad.Email = usuario.Email;
                usuarioEntidad.UsuarioNombre = usuario.UsuarioNombre;
                usuarioEntidad.Clave = usuario.Clave;
                usuarioEntidad.FechaCreacion = usuario.FechaCreacion;
                usuarioEntidad.FechaActualizacion = usuario.FechaActualizacion;

                if (Foto != null && Foto.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/Fotos");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Foto.FileName);
                    string fullPath = Path.Combine(folderPath, fileName);
                    Foto.SaveAs(fullPath);

                    usuarioEntidad.FotoPath = "/Fotos/" + fileName;
                }

                usuarioEntidad.RolID = usuario.RolID;

                db.Entry(usuarioEntidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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
