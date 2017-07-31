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

namespace copacker.Models
{
    public class EmpresasController : Controller
    {
        public EmpresasController()
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

        // GET: Empresas
        public async Task<ActionResult> Index()
        {
            return View(await db.Empresas.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idEmpresa,RazonSocial,CostoxHoraxOperario")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Empresas.Add(empresa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idEmpresa,RazonSocial,CostoxHoraxOperario")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Empresa empresa = await db.Empresas.FindAsync(id);
            db.Empresas.Remove(empresa);
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
