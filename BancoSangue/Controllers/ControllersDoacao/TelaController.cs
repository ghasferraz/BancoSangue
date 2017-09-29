using BancoSangue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BancoSangue.Controllers.ControllersDoacao
{
    public class TelaController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
            return View(db.Doacaos.ToList());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,DataInicio,DataFim,NumeroVagas,NomeResponsavel")] Doacao doacao)
		{
			if (ModelState.IsValid)
			{
				db.Doacaos.Add(doacao);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(doacao);
		}
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

	}

}
