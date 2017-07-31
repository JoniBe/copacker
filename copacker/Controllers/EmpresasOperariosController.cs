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
    public class EmpresasOperariosController : Controller
    {
        public EmpresasOperariosController()
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

        // GET: EmpresasOperarios
        public async Task<ActionResult> Index()
        {
            var empresasOperarios = db.EmpresasOperarios.Include(e => e.Empresa);
            return View(await empresasOperarios.ToListAsync());
        }

        // GET: EmpresasOperarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresasOperario empresasOperario = await db.EmpresasOperarios.FindAsync(id);
            if (empresasOperario == null)
            {
                return HttpNotFound();
            }
            return View(empresasOperario);
        }

        // GET: EmpresasOperarios/Create
        public ActionResult Create()
        {
            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "RazonSocial");
            return View();
        }

        // POST: EmpresasOperarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Legajo,idEmpresa,Apellidos,Nombres,Documento,Activo,FechaVtoCarnetSalud,CompletoCuestiorarioIngreso,CompletoCuestiorarioEnfermedades,Telefono1,Telefono2")] EmpresasOperario empresasOperario)
        {
            if (ModelState.IsValid)
            {
                db.EmpresasOperarios.Add(empresasOperario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "RazonSocial", empresasOperario.idEmpresa);
            return View(empresasOperario);
        }

        // GET: EmpresasOperarios/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresasOperario empresasOperario = await db.EmpresasOperarios.FindAsync(id);
            if (empresasOperario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "RazonSocial", empresasOperario.idEmpresa);
            return View(empresasOperario);
        }

        // POST: EmpresasOperarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Legajo,idEmpresa,Apellidos,Nombres,Documento,Activo,FechaVtoCarnetSalud,CompletoCuestiorarioIngreso,CompletoCuestiorarioEnfermedades,Telefono1,Telefono2")] EmpresasOperario empresasOperario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresasOperario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpresa = new SelectList(db.Empresas, "idEmpresa", "RazonSocial", empresasOperario.idEmpresa);
            return View(empresasOperario);
        }

        // GET: EmpresasOperarios/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresasOperario empresasOperario = await db.EmpresasOperarios.FindAsync(id);
            if (empresasOperario == null)
            {
                return HttpNotFound();
            }
            return View(empresasOperario);
        }

        // POST: EmpresasOperarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            EmpresasOperario empresasOperario = await db.EmpresasOperarios.FindAsync(id);
            db.EmpresasOperarios.Remove(empresasOperario);
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
