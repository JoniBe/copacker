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
    public class TurnoesController : Controller
    {
        public TurnoesController()
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

        private CopackerEntities db = new CopackerEntities();

        // GET: Turnoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Turnos.ToListAsync());
        }

        // GET: Turnoes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = await db.Turnos.FindAsync(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // GET: Turnoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Turnoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idTurno,Turno1")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Turnos.Add(turno);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(turno);
        }

        // GET: Turnoes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = await db.Turnos.FindAsync(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Turnoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idTurno,Turno1")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turno).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(turno);
        }

        // GET: Turnoes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = await db.Turnos.FindAsync(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Turnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Turno turno = await db.Turnos.FindAsync(id);
            db.Turnos.Remove(turno);
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
