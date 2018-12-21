namespace s4t.Erp.Fiscal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "s4tFiscal.NotaFiscal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Numero = c.String(),
                        Serie = c.String(),
                        DataEmissao = c.DateTime(nullable: false),
                        DataEntrada = c.DateTime(nullable: false),
                        DataSaida = c.DateTime(nullable: false),
                        DataES = c.DateTime(nullable: false),
                        NotaFiscalTipo_Value = c.Int(nullable: false),
                        NotaFiscalTipo_Name = c.String(),
                        NotaFiscalModoInclusao_Value = c.Int(nullable: false),
                        NotaFiscalModoInclusao_Name = c.String(),
                        EmitenteId = c.Guid(nullable: false),
                        DestinatarioId = c.Guid(nullable: false),
                        DestinatarioFazendaId = c.Guid(nullable: false),
                        Cfop_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tFiscal.CFOP", t => t.Cfop_Id)
                .Index(t => t.Cfop_Id);
            
            CreateTable(
                "s4tFiscal.CFOP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tFiscal.Produto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descricao = c.String(),
                        ProdutoUnidade_Id = c.Guid(),
                        NotaFiscal_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tFiscal.ProdutoUnidade", t => t.ProdutoUnidade_Id)
                .ForeignKey("s4tFiscal.NotaFiscal", t => t.NotaFiscal_Id)
                .Index(t => t.ProdutoUnidade_Id)
                .Index(t => t.NotaFiscal_Id);
            
            CreateTable(
                "s4tFiscal.ProdutoUnidade",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("s4tFiscal.Produto", "NotaFiscal_Id", "s4tFiscal.NotaFiscal");
            DropForeignKey("s4tFiscal.Produto", "ProdutoUnidade_Id", "s4tFiscal.ProdutoUnidade");
            DropForeignKey("s4tFiscal.NotaFiscal", "Cfop_Id", "s4tFiscal.CFOP");
            DropIndex("s4tFiscal.Produto", new[] { "NotaFiscal_Id" });
            DropIndex("s4tFiscal.Produto", new[] { "ProdutoUnidade_Id" });
            DropIndex("s4tFiscal.NotaFiscal", new[] { "Cfop_Id" });
            DropTable("s4tFiscal.ProdutoUnidade");
            DropTable("s4tFiscal.Produto");
            DropTable("s4tFiscal.CFOP");
            DropTable("s4tFiscal.NotaFiscal");
        }
    }
}
