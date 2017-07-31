using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;

namespace copacker.Controllers
{
    public class DotacionSolicitadasController : Controller
    {
        public DotacionSolicitadasController()
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

        // GET: DotacionSolicitadas
        public async Task<ActionResult> Index()
        {
            var dotacionSolicitadas = db.DotacionSolicitadas.Include(d => d.Turno);
            return View(await dotacionSolicitadas.ToListAsync());
        }

        // GET: DotacionSolicitadas/Details/5
        public async Task<ActionResult> Details(string id, string idTurno)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime fecha = DateTime.ParseExact(id,
                                        "yyyyMMdd",
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.None);

            var result =
                db.DotacionSolicitadas.Where(c => (c.Fecha == fecha))
                        .ToList();
            DotacionSolicitada dotacionSolicitada = result.Where(d => (d.idTurno == idTurno))
                .FirstOrDefault();
            //DotacionSolicitada dotacionSolicitada = await db.DotacionSolicitadas.FindAsync(id);
            if (dotacionSolicitada == null)
            {
                return HttpNotFound();
            }
            return View(dotacionSolicitada);
        }

        // GET: DotacionSolicitadas/Create
        public ActionResult Create()
        {
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Turno1");
            return View();
        }

        // POST: DotacionSolicitadas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Fecha,idTurno,DotacionSolicitada1")] DotacionSolicitada dotacionSolicitada)
        {
            if (ModelState.IsValid)
            {
                db.DotacionSolicitadas.Add(dotacionSolicitada);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Turno1", dotacionSolicitada.idTurno);
            return View(dotacionSolicitada);
        }

        // GET: DotacionSolicitadas/Edit/5
        public async Task<ActionResult> Edit(string id, string idTurno)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime fecha = DateTime.ParseExact(id,
                                        "yyyyMMdd",
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.None);

            var result =
                db.DotacionSolicitadas.Where(c => (c.Fecha == fecha))
                        .ToList();
            DotacionSolicitada dotacionSolicitada = result.Where(d => (d.idTurno == idTurno))
                .FirstOrDefault();
            //DotacionSolicitada dotacionSolicitada = await db.DotacionSolicitadas.FindAsync(id);
            if (dotacionSolicitada == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Turno1", dotacionSolicitada.idTurno);
            return View(dotacionSolicitada);
        }

        // POST: DotacionSolicitadas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Fecha,idTurno,DotacionSolicitada1")] DotacionSolicitada dotacionSolicitada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dotacionSolicitada).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Turno1", dotacionSolicitada.idTurno);
            return View(dotacionSolicitada);
        }

        // GET: DotacionSolicitadas/Delete/5
        public async Task<ActionResult> Delete(string id, string idTurno)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime fecha = DateTime.ParseExact(id,
                                        "yyyyMMdd",
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.None);

            var result =
                db.DotacionSolicitadas.Where(c => (c.Fecha == fecha))
                        .ToList();
            DotacionSolicitada dotacionSolicitada = result.Where(d => (d.idTurno == idTurno))
                .FirstOrDefault();

            //DotacionSolicitada dotacionSolicitada = await db.DotacionSolicitadas.FindAsync(new { fecha, idTurno });
            if (dotacionSolicitada == null)
            {
                return HttpNotFound();
            }
            return View(dotacionSolicitada);
        }

        // POST: DotacionSolicitadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string idTurno)
        {
            DateTime fecha = DateTime.ParseExact(id,
                                       "yyyyMMdd",
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None);

            var result =
                db.DotacionSolicitadas.Where(c => (c.Fecha == fecha))
                        .ToList();
            DotacionSolicitada dotacionSolicitada = result.Where(d => (d.idTurno == idTurno))
                .FirstOrDefault();

            //DotacionSolicitada dotacionSolicitada = await db.DotacionSolicitadas.FindAsync(id);
            db.DotacionSolicitadas.Remove(dotacionSolicitada);
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
