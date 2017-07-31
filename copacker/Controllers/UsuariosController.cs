using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using copacker;

namespace copacker.Controllers
{
    public class UsuariosController : Controller
    {
        public UsuariosController()
        {
            CopackerSeguridad sec = new CopackerSeguridad();
            string strUser = System.Threading.Thread.CurrentPrincipal.Identity.Name.ToLower();
            strUser = strUser.Split('\\')[1].ToLower();
            var results = sec.Usuarios.Where(c => c.idUsuario.ToLower() == strUser)
                 .Select(x => x.idPerfil)
                 .ToList();
            if (results.Count > 0)
            {
                string _idGrupo = results[0].ToString();
                var grupos = sec.Perfiles.Where(g => g.idPerfil == _idGrupo)
                 .Select(y => y.idPerfil)
                 .ToList();
                ViewBag.UserRoleProperty = grupos[0].ToString();
            }
            else
            {
                ViewBag.UserRoleProperty = "Reject";
            }

        }

        private CopackerSeguridad db = new CopackerSeguridad();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Perfil);
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = await db.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.idPerfil = new SelectList(db.Perfiles, "idPerfil", "idPerfil");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idUsuario,idPerfil")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idPerfil = new SelectList(db.Perfiles, "idPerfil", "idPerfil", usuario.idPerfil);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPerfil = new SelectList(db.Perfiles, "idPerfil", "idPerfil", usuario.idPerfil);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idUsuario,idGrupo,Usuario1")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupo = new SelectList(db.Perfiles, "idGrupo", "Grupo", usuario.idPerfil);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Usuarios usuario = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();
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
