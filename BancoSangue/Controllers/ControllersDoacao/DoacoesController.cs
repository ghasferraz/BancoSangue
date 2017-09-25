using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BancoSangue.Models;
using System.IO;

namespace BancoSangue.Controllers.ControllersDoacao
{
    public class DoacoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Doacoes
        public ActionResult Index()
        {
            return View(db.Doacaos.ToList());
        }

        // GET: Doacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doacao doacao = db.Doacaos.Find(id);
            if (doacao == null)
            {
                return HttpNotFound();
            }
            return View(doacao);
        }

        // GET: Doacoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doacao doacao)
        {



			if (ModelState.IsValid)
            {
				int arquivosSalvos = 0;
				
				for (int i = 0; i < Request.Files.Count; i++)
				{
					HttpPostedFileBase arquivo = Request.Files[i];
					if (arquivo.ContentLength > 0)
					{

						var UploadPath = Server.MapPath("~/Content");
						string caminhoArquivo = Path.Combine(@UploadPath, Path.GetFileName(arquivo.FileName));
						

						arquivo.SaveAs(caminhoArquivo);
						arquivosSalvos++;

					}
				}
				ViewData["Message"] = String.Format("{0} arquivo salvo com sucesso", arquivosSalvos);
				db.Doacaos.Add(doacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doacao);
        }
		public ActionResult _AddImagem(List <Imagem> img)
		{
			 img=  new List<Imagem>();
			for (int i= 0; i <img.Count; i++)
			{
				img.Add(new Imagem());
			}
			return PartialView(img);
		}


        // GET: Doacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doacao doacao = db.Doacaos.Find(id);
            if (doacao == null)
            {
                return HttpNotFound();
            }
            return View(doacao);
        }

        // POST: Doacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,DataInicio,DataFim,NumeroVagas,NomeResponsavel")] Doacao doacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doacao);
        }

        // GET: Doacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doacao doacao = db.Doacaos.Find(id);
            if (doacao == null)
            {
                return HttpNotFound();
            }
            return View(doacao);
        }

        // POST: Doacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doacao doacao = db.Doacaos.Find(id);
            db.Doacaos.Remove(doacao);
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
