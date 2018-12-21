namespace s4t.Erp.Graos.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoletimLoteRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("s4tGraos.Boletim", "TipoGrao_Value", c => c.Int(nullable: false));
            AddColumn("s4tGraos.Boletim", "TipoGrao_Name", c => c.String());
            CreateIndex("s4tGraos.Boletim", "LoteId");
            AddForeignKey("s4tGraos.Boletim", "LoteId", "s4tGraos.Lote", "Id");
            DropColumn("s4tGraos.Boletim", "LoteNumero");
        }
        
        public override void Down()
        {
            AddColumn("s4tGraos.Boletim", "LoteNumero", c => c.String());
            DropForeignKey("s4tGraos.Boletim", "LoteId", "s4tGraos.Lote");
            DropIndex("s4tGraos.Boletim", new[] { "LoteId" });
            DropColumn("s4tGraos.Boletim", "TipoGrao_Name");
            DropColumn("s4tGraos.Boletim", "TipoGrao_Value");
        }
    }
}
