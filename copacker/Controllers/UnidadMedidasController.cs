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
    public class UnidadMedidasController : Controller
    {
        public UnidadMedidasController()
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

        // GET: UnidadMedidas
        public async Task<ActionResult> Index()
        {
            return View(await db.UnidadMedidas.ToListAsync());
        }

        // GET: UnidadMedidas/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadMedida unidadMedida = await db.UnidadMedidas.FindAsync(id);
            if (unidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(unidadMedida);
        }

        // GET: UnidadMedidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnidadMedidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idUniMed,Descripcion")] UnidadMedida unidadMedida)
        {
            if (ModelState.IsValid)
            {
                db.UnidadMedidas.Add(unidadMedida);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unidadMedida);
        }

        // GET: UnidadMedidas/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadMedida unidadMedida = await db.UnidadMedidas.FindAsync(id);
            if (unidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(unidadMedida);
        }

        // POST: UnidadMedidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idUniMed,Descripcion")] UnidadMedida unidadMedida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unidadMedida).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unidadMedida);
        }

        // GET: UnidadMedidas/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadMedida unidadMedida = await db.UnidadMedidas.FindAsync(id);
            if (unidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(unidadMedida);
        }

        // POST: UnidadMedidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            UnidadMedida unidadMedida = await db.UnidadMedidas.FindAsync(id);
            db.UnidadMedidas.Remove(unidadMedida);
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
