namespace s4t.Erp.Graos.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "s4tGraos.Armazem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Codigo = c.String(),
                        EnderecoLogradouro = c.String(),
                        EnderecoNumero = c.String(),
                        EnderecoComplemento = c.String(),
                        EnderecoBairro = c.String(),
                        Endereco = c.String(),
                        Cep_Numero = c.String(),
                        CidadeId = c.Guid(nullable: false),
                        Altura = c.Int(nullable: false),
                        Capacidade = c.Int(nullable: false),
                        Observacao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.Boletim",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Numero = c.String(),
                        Data = c.DateTime(nullable: false),
                        Item = c.String(),
                        BoletimDocumento_Serie_Value = c.String(),
                        BoletimDocumento_DocumentoEntradaId = c.Guid(nullable: false),
                        BoletimDocumento_InstrucaoServicoId = c.Guid(nullable: false),
                        BoletimDocumento_InstrucaoServico_FilialId = c.Guid(nullable: false),
                        BoletimDocumento_InstrucaoServico_Numero = c.Int(nullable: false),
                        BoletimDocumento_InstrucaoServico_Data = c.DateTime(nullable: false),
                        BoletimDocumento_InstrucaoServico_Id = c.Guid(nullable: false),
                        BoletimDocumento_TransferenciaId = c.Guid(nullable: false),
                        BoletimDocumento_Transferencia_FilialId = c.Guid(nullable: false),
                        BoletimDocumento_Transferencia_Numero = c.Int(nullable: false),
                        BoletimDocumento_Transferencia_Data = c.DateTime(nullable: false),
                        BoletimDocumento_Transferencia_Id = c.Guid(nullable: false),
                        BoletimDocumento_OrdemCarregamentoId = c.Guid(nullable: false),
                        BoletimDocumento_OrdemCarregamento_FilialId = c.Guid(nullable: false),
                        BoletimDocumento_OrdemCarregamento_Numero = c.Int(nullable: false),
                        BoletimDocumento_OrdemCarregamento_Data = c.DateTime(nullable: false),
                        BoletimDocumento_OrdemCarregamento_Id = c.Guid(nullable: false),
                        BoletimDocumento_RemocaoNumero = c.Int(nullable: false),
                        LoteNumero = c.String(),
                        LoteId = c.Guid(nullable: false),
                        Sacas = c.Int(nullable: false),
                        Origem_FilialId = c.Guid(nullable: false),
                        Origem_ArmazemId = c.Guid(nullable: false),
                        Origem_Quadra = c.String(),
                        Origem_Bloco = c.String(),
                        Origem_LocalId = c.Guid(nullable: false),
                        Destino_FilialId = c.Guid(nullable: false),
                        Destino_ArmazemId = c.Guid(nullable: false),
                        Destino_Quadra = c.String(),
                        Destino_Bloco = c.String(),
                        Destino_LocalId = c.Guid(nullable: false),
                        LoteUltimoId = c.Guid(nullable: false),
                        LoteUltimoSacas = c.Int(nullable: false),
                        ES = c.String(),
                        IS = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.DocumentoEntradaEmbalagem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DocumentoEntradaId = c.Guid(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Embalagem_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.DocumentoEntrada", t => t.DocumentoEntradaId)
                .ForeignKey("s4tGraos.Embalagem", t => t.Embalagem_Id)
                .Index(t => t.DocumentoEntradaId)
                .Index(t => t.Embalagem_Id);
            
            CreateTable(
                "s4tGraos.DocumentoEntrada",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Numero = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        TipoOperacao_Value = c.Int(nullable: false),
                        TipoOperacao_Name = c.String(),
                        DocumentoEntradaStatus_Value = c.Int(nullable: false),
                        DocumentoEntradaStatus_Name = c.String(),
                        TicketPesagemId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.TicketPesagem", t => t.TicketPesagemId)
                .ForeignKey("s4tGraos.NotaFiscalGraos", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.TicketPesagemId);
            
            CreateTable(
                "s4tGraos.Lote",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Numero = c.String(),
                        Sacas = c.Int(nullable: false),
                        Peso = c.Double(nullable: false),
                        TipoGrao_Value = c.Int(nullable: false),
                        TipoGrao_Name = c.String(),
                        Localizacao_FilialId = c.Guid(nullable: false),
                        Localizacao_ArmazemId = c.Guid(nullable: false),
                        Localizacao_Quadra = c.String(),
                        Localizacao_Bloco = c.String(),
                        Localizacao_LocalId = c.Guid(nullable: false),
                        CadastroTitularId = c.Guid(nullable: false),
                        FazendaTitularId = c.Guid(nullable: false),
                        DocumentoEntrada_Id = c.Guid(),
                        Embalagem_Id = c.Guid(),
                        TicketPesagemMovimentacaoId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.DocumentoEntrada", t => t.DocumentoEntrada_Id)
                .ForeignKey("s4tGraos.Embalagem", t => t.Embalagem_Id)
                .ForeignKey("s4tGraos.TicketPesagemMovimentacao", t => t.TicketPesagemMovimentacaoId)
                .Index(t => t.DocumentoEntrada_Id)
                .Index(t => t.Embalagem_Id)
                .Index(t => t.TicketPesagemMovimentacaoId);
            
            CreateTable(
                "s4tGraos.Embalagem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descricao = c.String(),
                        Capacidade = c.Double(nullable: false),
                        Peso = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.LoteEmbalagemNumeracao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Numero = c.String(),
                        Lote_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.Lote", t => t.Lote_Id)
                .Index(t => t.Lote_Id);
            
            CreateTable(
                "s4tGraos.TicketPesagemMovimentacao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketPesagemId = c.Guid(nullable: false),
                        Ordem = c.Int(nullable: false),
                        Peso = c.Double(nullable: false),
                        PesoDiferencialAnterior = c.Double(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        LoteId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.TicketPesagem", t => t.TicketPesagemId)
                .ForeignKey("s4tGraos.Lote", t => t.LoteId)
                .Index(t => t.TicketPesagemId)
                .Index(t => t.LoteId);
            
            CreateTable(
                "s4tGraos.TicketPesagem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Numero = c.String(),
                        TicketPortaria_Id = c.Guid(),
                        DocumentoEntradaId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.TicketPortaria", t => t.TicketPortaria_Id)
                .ForeignKey("s4tGraos.DocumentoEntrada", t => t.DocumentoEntradaId)
                .Index(t => t.TicketPortaria_Id)
                .Index(t => t.DocumentoEntradaId);
            
            CreateTable(
                "s4tGraos.TicketPortaria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Numero = c.String(),
                        TipoServicoPortaria_Value = c.Int(nullable: false),
                        TipoServicoPortaria_Name = c.String(),
                        RegistroDePortaria_Id = c.Guid(),
                        RegistroDePortariaServicoAvulso_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.RegistroDePortaria", t => t.RegistroDePortaria_Id)
                .ForeignKey("s4tGraos.RegistroDePortariaServicoAvulso", t => t.RegistroDePortariaServicoAvulso_Id)
                .Index(t => t.RegistroDePortaria_Id)
                .Index(t => t.RegistroDePortariaServicoAvulso_Id);
            
            CreateTable(
                "s4tGraos.RegistroDePortaria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        TipoGrao_Value = c.Int(nullable: false),
                        TipoGrao_Name = c.String(),
                        TipoOperacaoPortaria_Value = c.Int(nullable: false),
                        TipoOperacaoPortaria_Name = c.String(),
                        Placa_Numero = c.String(),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.NotaFiscalGraos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistroDePortariaId = c.Guid(),
                        NotaFiscalId = c.Guid(nullable: false),
                        NotaFiscalNumero = c.String(),
                        NotaFiscalSerie = c.String(),
                        NotaFiscalDataEmissao = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NotaFiscalDataSaida = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NotaFiscalOperacaoFiscalCodigo = c.String(),
                        NotaFiscalOperacaoFiscalDescricao = c.String(),
                        NotaFiscalObservacoes = c.String(),
                        NotaFiscalGraosTipo = c.Int(nullable: false),
                        NotaFiscalGraosTipoDescricao = c.String(),
                        TipoGrao = c.Int(nullable: false),
                        EmpresaCodigo = c.Int(nullable: false),
                        EmpresaNome = c.String(),
                        FilialId = c.Guid(nullable: false),
                        FilialCodigo = c.Int(nullable: false),
                        FilialDescricao = c.String(),
                        FilialEndereco = c.String(),
                        FilialCidade = c.String(),
                        FilialUf = c.String(),
                        FilialCnpj = c.String(),
                        FilialInscricaoEstadual = c.String(),
                        DestinatarioCadastroId = c.Guid(nullable: false),
                        DestinatarioCodigo = c.Int(nullable: false),
                        DestinatarioNome = c.String(),
                        DestinatarioCpf = c.String(),
                        DestinatarioFazendaId = c.Guid(nullable: false),
                        DestinatarioFazendaCodigo = c.Int(nullable: false),
                        DestinatarioFazendaNome = c.String(),
                        DestinatarioFazendaInscricaoEstadual = c.String(),
                        DestinatarioFazendaCnpj = c.String(),
                        DestinatarioFazendaMunicipio = c.String(),
                        DestinatarioFazendaUf = c.String(),
                        PesoLiquido = c.Double(nullable: false),
                        EmbalagemId = c.Guid(nullable: false),
                        EmbalagemCodigo = c.Int(nullable: false),
                        EmbalagemDescricao = c.String(),
                        EmbalagemQuantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.RegistroDePortaria", t => t.RegistroDePortariaId)
                .Index(t => t.RegistroDePortariaId);
            
            CreateTable(
                "s4tGraos.NotaFiscalGraosCertificado",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CertificadoEmissorId = c.Guid(nullable: false),
                        CertificadoEmissorCodigo = c.Int(nullable: false),
                        CertificadoEmissorNome = c.String(),
                        CertificacaoNumero = c.String(),
                        NotaFiscalGraos_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.NotaFiscalGraos", t => t.NotaFiscalGraos_Id)
                .Index(t => t.NotaFiscalGraos_Id);
            
            CreateTable(
                "s4tGraos.RegistroDePortariaServicoAvulso",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        Placa_Numero = c.String(),
                        NomeDoMotoristaSemCadastro = c.String(),
                        Data = c.DateTime(nullable: false),
                        ProdutoPortaria_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tGraos.ProdutoPortaria", t => t.ProdutoPortaria_Id)
                .Index(t => t.ProdutoPortaria_Id);
            
            CreateTable(
                "s4tGraos.ProdutoPortaria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.Local",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmpresaId = c.Guid(nullable: false),
                        Codigo = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.Pilha",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilialId = c.Guid(nullable: false),
                        ArmazemId = c.Guid(nullable: false),
                        Quadra = c.String(),
                        Bloco = c.String(),
                        MontaPilha = c.Boolean(nullable: false),
                        NumeroBase = c.Int(nullable: false),
                        NumeroAltura = c.Int(nullable: false),
                        NumeroLastro = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "s4tGraos.RegistroDePortariaLog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistroDePortariaId = c.Guid(nullable: false),
                        RegistroDePortariaServicoAvulsoId = c.Guid(nullable: false),
                        FilialCodigo = c.Int(nullable: false),
                        FilialNome = c.String(),
                        TipoGrao = c.Int(nullable: false),
                        TipoGraoDescricao = c.String(),
                        TipoOperacao = c.Int(nullable: false),
                        TipoOperacaoDescricao = c.String(),
                        Placa = c.String(),
                        MotoristaCodigo = c.Int(nullable: false),
                        MotoristaNome = c.String(),
                        NomeDoMotoristaSemCadastro = c.String(),
                        ProdutoPortariaCodigo = c.Int(nullable: false),
                        ProdutoPortariaDescricao = c.String(),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("s4tGraos.DocumentoEntradaEmbalagem", "Embalagem_Id", "s4tGraos.Embalagem");
            DropForeignKey("s4tGraos.DocumentoEntradaEmbalagem", "DocumentoEntradaId", "s4tGraos.DocumentoEntrada");
            DropForeignKey("s4tGraos.TicketPesagem", "DocumentoEntradaId", "s4tGraos.DocumentoEntrada");
            DropForeignKey("s4tGraos.TicketPesagemMovimentacao", "LoteId", "s4tGraos.Lote");
            DropForeignKey("s4tGraos.TicketPesagemMovimentacao", "TicketPesagemId", "s4tGraos.TicketPesagem");
            DropForeignKey("s4tGraos.TicketPesagem", "TicketPortaria_Id", "s4tGraos.TicketPortaria");
            DropForeignKey("s4tGraos.TicketPortaria", "RegistroDePortariaServicoAvulso_Id", "s4tGraos.RegistroDePortariaServicoAvulso");
            DropForeignKey("s4tGraos.RegistroDePortariaServicoAvulso", "ProdutoPortaria_Id", "s4tGraos.ProdutoPortaria");
            DropForeignKey("s4tGraos.TicketPortaria", "RegistroDePortaria_Id", "s4tGraos.RegistroDePortaria");
            DropForeignKey("s4tGraos.NotaFiscalGraos", "RegistroDePortariaId", "s4tGraos.RegistroDePortaria");
            DropForeignKey("s4tGraos.NotaFiscalGraosCertificado", "NotaFiscalGraos_Id", "s4tGraos.NotaFiscalGraos");
            DropForeignKey("s4tGraos.DocumentoEntrada", "Id", "s4tGraos.NotaFiscalGraos");
            DropForeignKey("s4tGraos.DocumentoEntrada", "TicketPesagemId", "s4tGraos.TicketPesagem");
            DropForeignKey("s4tGraos.Lote", "TicketPesagemMovimentacaoId", "s4tGraos.TicketPesagemMovimentacao");
            DropForeignKey("s4tGraos.LoteEmbalagemNumeracao", "Lote_Id", "s4tGraos.Lote");
            DropForeignKey("s4tGraos.Lote", "Embalagem_Id", "s4tGraos.Embalagem");
            DropForeignKey("s4tGraos.Lote", "DocumentoEntrada_Id", "s4tGraos.DocumentoEntrada");
            DropIndex("s4tGraos.RegistroDePortariaServicoAvulso", new[] { "ProdutoPortaria_Id" });
            DropIndex("s4tGraos.NotaFiscalGraosCertificado", new[] { "NotaFiscalGraos_Id" });
            DropIndex("s4tGraos.NotaFiscalGraos", new[] { "RegistroDePortariaId" });
            DropIndex("s4tGraos.TicketPortaria", new[] { "RegistroDePortariaServicoAvulso_Id" });
            DropIndex("s4tGraos.TicketPortaria", new[] { "RegistroDePortaria_Id" });
            DropIndex("s4tGraos.TicketPesagem", new[] { "DocumentoEntradaId" });
            DropIndex("s4tGraos.TicketPesagem", new[] { "TicketPortaria_Id" });
            DropIndex("s4tGraos.TicketPesagemMovimentacao", new[] { "LoteId" });
            DropIndex("s4tGraos.TicketPesagemMovimentacao", new[] { "TicketPesagemId" });
            DropIndex("s4tGraos.LoteEmbalagemNumeracao", new[] { "Lote_Id" });
            DropIndex("s4tGraos.Lote", new[] { "TicketPesagemMovimentacaoId" });
            DropIndex("s4tGraos.Lote", new[] { "Embalagem_Id" });
            DropIndex("s4tGraos.Lote", new[] { "DocumentoEntrada_Id" });
            DropIndex("s4tGraos.DocumentoEntrada", new[] { "TicketPesagemId" });
            DropIndex("s4tGraos.DocumentoEntrada", new[] { "Id" });
            DropIndex("s4tGraos.DocumentoEntradaEmbalagem", new[] { "Embalagem_Id" });
            DropIndex("s4tGraos.DocumentoEntradaEmbalagem", new[] { "DocumentoEntradaId" });
            DropTable("s4tGraos.RegistroDePortariaLog");
            DropTable("s4tGraos.Pilha");
            DropTable("s4tGraos.Local");
            DropTable("s4tGraos.ProdutoPortaria");
            DropTable("s4tGraos.RegistroDePortariaServicoAvulso");
            DropTable("s4tGraos.NotaFiscalGraosCertificado");
            DropTable("s4tGraos.NotaFiscalGraos");
            DropTable("s4tGraos.RegistroDePortaria");
            DropTable("s4tGraos.TicketPortaria");
            DropTable("s4tGraos.TicketPesagem");
            DropTable("s4tGraos.TicketPesagemMovimentacao");
            DropTable("s4tGraos.LoteEmbalagemNumeracao");
            DropTable("s4tGraos.Embalagem");
            DropTable("s4tGraos.Lote");
            DropTable("s4tGraos.DocumentoEntrada");
            DropTable("s4tGraos.DocumentoEntradaEmbalagem");
            DropTable("s4tGraos.Boletim");
            DropTable("s4tGraos.Armazem");
        }
    }
}
