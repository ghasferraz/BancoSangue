using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BancoSangue.Models
{
	public class Doacao
	{
		[Key]
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		[Display (Name ="Data de Início")]
		public DateTime DataInicio { get; set; }
		[Display(Name = "Data Final")]
		public DateTime DataFim { get; set; }
		public int NumeroVagas { get; set; }
		public string NomeResponsavel { get; set; }
		
		public virtual IList<Imagem> imagem {get;set;}
		

	}
}