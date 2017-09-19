namespace BancoSangue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicaodelegenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Imagems", "caminho", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Imagems", "caminho");
        }
    }
}
