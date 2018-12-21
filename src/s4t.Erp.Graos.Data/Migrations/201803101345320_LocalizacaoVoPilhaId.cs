namespace s4t.Erp.Graos.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalizacaoVoPilhaId : DbMigration
    {
        public override void Up()
        {
            AddColumn("s4tGraos.Boletim", "Origem_PilhaId", c => c.Guid(nullable: false));
            AddColumn("s4tGraos.Boletim", "Destino_PilhaId", c => c.Guid(nullable: false));
            AddColumn("s4tGraos.Lote", "Localizacao_PilhaId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("s4tGraos.Lote", "Localizacao_PilhaId");
            DropColumn("s4tGraos.Boletim", "Destino_PilhaId");
            DropColumn("s4tGraos.Boletim", "Origem_PilhaId");
        }
    }
}
