using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BancoSangue.Models;

namespace BancoSangue.Controllers.ControllersDoacao
{
    public class ImagensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Imagens
        public ActionResult Index()
        {
            var imagems = db.Imagems.Include(i => i.doacao);
            return View(imagems.ToList());
        }

        // GET: Imagens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagem imagem = db.Imagems.Find(id);
            if (imagem == null)
            {
                return HttpNotFound();
            }
            return View(imagem);
        }

        // GET: Imagens/Create
        public ActionResult Create()
        {
            ViewBag.IdDoacao = new SelectList(db.Doacaos, "Id", "Titulo");
            return View();
        }

        // POST: Imagens/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdDoacao,Legenda")] Imagem imagem)
        {
            if (ModelState.IsValid)
            {
                db.Imagems.Add(imagem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDoacao = new SelectList(db.Doacaos, "Id", "Titulo", imagem.IdDoacao);
            return View(imagem);
        }

        // GET: Imagens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagem imagem = db.Imagems.Find(id);
            if (imagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDoacao = new SelectList(db.Doacaos, "Id", "Titulo", imagem.IdDoacao);
            return View(imagem);
        }

        // POST: Imagens/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdDoacao,Legenda")] Imagem imagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDoacao = new SelectList(db.Doacaos, "Id", "Titulo", imagem.IdDoacao);
            return View(imagem);
        }

        // GET: Imagens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagem imagem = db.Imagems.Find(id);
            if (imagem == null)
            {
                return HttpNotFound();
            }
            return View(imagem);
        }

        // POST: Imagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imagem imagem = db.Imagems.Find(id);
            db.Imagems.Remove(imagem);
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
