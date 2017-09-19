namespace BancoSangue.Migrations
{
	using BancoSangue.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BancoSangue.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(BancoSangue.Models.ApplicationDbContext context) =>
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			context.Doacaos.AddOrUpdate(
				d => d.Titulo,
				new Doacao { Titulo = "Doacao de Sangue",
					DataInicio =DateTime.Now,
					DataFim =DateTime.Now,
					Descricao = "Doacao de Sangue",
					NumeroVagas =20,
					NomeResponsavel ="Joao", imagem=new List <Imagem>{ new Imagem {caminho="1.jpg",Legenda="Imagem 1" } } }

		
		);
	}
}
