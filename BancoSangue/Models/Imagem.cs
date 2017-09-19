using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BancoSangue.Models
{
	public class Imagem
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("doacao")]
		public int IdDoacao { get; set; }
		public string caminho { get; set; }
		public string Legenda { get; set; }
		public virtual Doacao doacao { get; set; }

	}
}