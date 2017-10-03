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
using System.Drawing;
using System.Text.RegularExpressions;

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

        public ActionResult _Imagem(int id, Image img)
        {
            ViewBag.Index = id;
            return PartialView(img);
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
				try
				{
					IList<Imagem> img = new List<Imagem>();
					foreach (var imagem in doacao.imagem)
					{
						img.Add(imagem);

					}
					for (int i = 0; i < Request.Files.Count; i++)
					{
						HttpPostedFileBase file = Request.Files[i];
						if (file != null && file.ContentLength > 0 && ((file.ContentType == "image/jpeg") || (file.ContentType == "image/jpg") || (file.ContentType == "image/png")))
						{
							var PathArquivo = Path.Combine(Server.MapPath("~/Content"));
							var NomeArquivo = Path.GetFileName(file.FileName);
							var ArquivoAntigo = Path.GetFileName(file.FileName);
							var Extensao = Path.GetExtension(file.FileName);
							var NomeSemExtensao = Path.GetFileNameWithoutExtension(file.FileName);

							var indice = 1;
							while (System.IO.File.Exists(Path.Combine(PathArquivo, NomeArquivo)))
							{
								NomeArquivo = Path.GetFileNameWithoutExtension(file.FileName) + "(" + indice + ")" + Extensao;
								indice++;

							}

							file.SaveAs(Path.Combine(PathArquivo, NomeArquivo));
							img.Where(m => m.caminho == ArquivoAntigo).FirstOrDefault().caminho = NomeArquivo;

						}

					}
					ViewData["Message"] = String.Format("{0} arquivo salvo com sucesso", arquivosSalvos);
					db.Doacaos.Add(doacao);
					db.SaveChanges();
					return RedirectToAction("Index");
				}


				catch (Exception)
				{

					throw;
				}


			}
			return View(doacao);
		}
		public ActionResult _AddImagem(List<Imagem> img)
		{
			img = new List<Imagem>();
			for (int i = 0; i < img.Count; i++)
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
		public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,DataInicio,DataFim,NumeroVagas,NomeResponsavel,Imagem,Imagens")] Doacao doacao)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var antigo = db.Doacaos.Include(m => m.imagem).Where(m => m.Id == doacao.Id).FirstOrDefault();
					if (antigo != null)
					{
						db.Imagems.RemoveRange(antigo.imagem);
						db.Entry(antigo).State = EntityState.Modified;



						IList<Imagem> img = new List<Imagem>();
						foreach (var imagem in doacao.imagem)
						{
							img.Add(imagem);
						}
						for (int i = 0; i < Request.Files.Count; i++)
						{

							HttpPostedFileBase file = Request.Files[i];
							if (file != null && file.ContentLength > 0 && ((file.ContentType == "image/jpeg") || (file.ContentType == "image/jpg") || (file.ContentType == "image/png")))
							{
								var PathArquivo = Path.Combine(Server.MapPath("~/Content"));
								var NomeArquivo = Path.GetFileName(file.FileName);
								var ArquivoAntigo = Path.GetFileName(file.FileName);
								var Extensao = Path.GetExtension(file.FileName);
								var NomeSemExtensao = Path.GetFileNameWithoutExtension(file.FileName);

								var indice = 1;
								while (System.IO.File.Exists(Path.Combine(PathArquivo, NomeArquivo)))
								{
									NomeArquivo = Path.GetFileNameWithoutExtension(file.FileName) + "(" + indice + ")" + Extensao;
									indice++;

								}

								file.SaveAs(Path.Combine(PathArquivo, NomeArquivo));
								img.Where(m => m.caminho == ArquivoAntigo).FirstOrDefault().caminho = NomeArquivo;

							}
						}

						foreach (var i in img)
						{
							antigo.imagem.Add(i);
						}
                        antigo.Titulo = doacao.Titulo;
						antigo.NomeResponsavel = doacao.NomeResponsavel;
						antigo.Descricao = doacao.Descricao;
						antigo.DataFim = doacao.DataFim;
						antigo.DataInicio = doacao.DataInicio;

						db.Entry(antigo).State = EntityState.Modified;
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}


				catch (Exception ex)
				{


					throw ex;
				}
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
