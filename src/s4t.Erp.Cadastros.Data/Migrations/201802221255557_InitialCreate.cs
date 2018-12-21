namespace s4t.Erp.Cadastros.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "s4tCadastros.Cadastro",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                        TipoPfPj = c.String(),
                        NumeroCnpj_Numero = c.String(),
                        NumeroCpf_Numero = c.String(),
                        NumeroRg = c.String(),
                        NumeroInscricaoEstadual = c.String(),
                        EnderecoLogradouro = c.String(),
                        EnderecoNumero = c.String(),
                        EnderecoComplemento = c.String(),
                        EnderecoBairro = c.String(),
                        Endereco = c.String(),
                        Cep_Numero = c.String(),
                        Email_Endereco = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        NomeFantasia = c.String(),
                        Cidade_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.Cidade", t => t.Cidade_Id)
                .Index(t => t.Cidade_Id);
            
            CreateTable(
                "s4tCadastros.Cidade",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        CodigoIbge = c.String(),
                        Nome = c.String(),
                        Uf_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.UF", t => t.Uf_Id)
                .Index(t => t.Uf_Id);
            
            CreateTable(
                "s4tCadastros.UF",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descricao = c.String(),
                        CodigoIbge = c.String(),
                        Pais_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.Pais", t => t.Pais_Id)
                .Index(t => t.Pais_Id);
            
            CreateTable(
                "s4tCadastros.Pais",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                        CodigoIbge = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tCadastros.Fazenda",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                        Cnpj_Numero = c.String(),
                        EnderecoLogradouro = c.String(),
                        EnderecoNumero = c.String(),
                        EnderecoComplemento = c.String(),
                        EnderecoBairro = c.String(),
                        Endereco = c.String(),
                        Cep_Numero = c.String(),
                        Telefone_Numero = c.String(),
                        TelefoneAux_Numero = c.String(),
                        CodigoInscricaoEstadualProdutorRural = c.String(),
                        DataValidadeCartaoProdutorRural = c.DateTime(nullable: false),
                        FazendaStatus_Value = c.Int(nullable: false),
                        FazendaStatus_Name = c.String(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataBaixa = c.DateTime(nullable: false),
                        Observacao = c.String(),
                        Cadastro_Id = c.Guid(),
                        Cidade_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.Cadastro", t => t.Cadastro_Id)
                .ForeignKey("s4tCadastros.Cidade", t => t.Cidade_Id)
                .Index(t => t.Cadastro_Id)
                .Index(t => t.Cidade_Id);
            
            CreateTable(
                "s4tCadastros.FazendaCertificacao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FazendaId = c.Guid(nullable: false),
                        CertificacaoNumero = c.String(),
                        FazendaCertificacaoTipoCultura_Value = c.Int(nullable: false),
                        FazendaCertificacaoTipoCultura_Name = c.String(),
                        PeriodoVigencia_Start = c.DateTime(nullable: false),
                        PeriodoVigencia_End = c.DateTime(nullable: false),
                        CertificacaoEmissor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.FazendaCertificacaoEmissor", t => t.CertificacaoEmissor_Id)
                .ForeignKey("s4tCadastros.Fazenda", t => t.FazendaId)
                .Index(t => t.FazendaId)
                .Index(t => t.CertificacaoEmissor_Id);
            
            CreateTable(
                "s4tCadastros.FazendaCertificacaoEmissor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tCadastros.Usuario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(),
                        Senha = c.String(),
                        UsuarioAspNetIdentityId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.Cadastro", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "s4tCadastros.Empresa",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tCadastros.Filial",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Nome = c.String(),
                        Cnpj_Numero = c.String(),
                        EmpresaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tCadastros.Empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("s4tCadastros.Filial", "EmpresaId", "s4tCadastros.Empresa");
            DropForeignKey("s4tCadastros.Usuario", "Id", "s4tCadastros.Cadastro");
            DropForeignKey("s4tCadastros.Fazenda", "Cidade_Id", "s4tCadastros.Cidade");
            DropForeignKey("s4tCadastros.FazendaCertificacao", "FazendaId", "s4tCadastros.Fazenda");
            DropForeignKey("s4tCadastros.FazendaCertificacao", "CertificacaoEmissor_Id", "s4tCadastros.FazendaCertificacaoEmissor");
            DropForeignKey("s4tCadastros.Fazenda", "Cadastro_Id", "s4tCadastros.Cadastro");
            DropForeignKey("s4tCadastros.Cadastro", "Cidade_Id", "s4tCadastros.Cidade");
            DropForeignKey("s4tCadastros.UF", "Pais_Id", "s4tCadastros.Pais");
            DropForeignKey("s4tCadastros.Cidade", "Uf_Id", "s4tCadastros.UF");
            DropIndex("s4tCadastros.Filial", new[] { "EmpresaId" });
            DropIndex("s4tCadastros.Usuario", new[] { "Id" });
            DropIndex("s4tCadastros.FazendaCertificacao", new[] { "CertificacaoEmissor_Id" });
            DropIndex("s4tCadastros.FazendaCertificacao", new[] { "FazendaId" });
            DropIndex("s4tCadastros.Fazenda", new[] { "Cidade_Id" });
            DropIndex("s4tCadastros.Fazenda", new[] { "Cadastro_Id" });
            DropIndex("s4tCadastros.UF", new[] { "Pais_Id" });
            DropIndex("s4tCadastros.Cidade", new[] { "Uf_Id" });
            DropIndex("s4tCadastros.Cadastro", new[] { "Cidade_Id" });
            DropTable("s4tCadastros.Filial");
            DropTable("s4tCadastros.Empresa");
            DropTable("s4tCadastros.Usuario");
            DropTable("s4tCadastros.FazendaCertificacaoEmissor");
            DropTable("s4tCadastros.FazendaCertificacao");
            DropTable("s4tCadastros.Fazenda");
            DropTable("s4tCadastros.Pais");
            DropTable("s4tCadastros.UF");
            DropTable("s4tCadastros.Cidade");
            DropTable("s4tCadastros.Cadastro");
        }
    }
}
