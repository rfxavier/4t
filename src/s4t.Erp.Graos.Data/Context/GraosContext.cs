using s4t.Erp.Graos.Data.EntityConfig.Balanca;
using s4t.Erp.Graos.Data.EntityConfig.Nucleo;
using s4t.Erp.Graos.Data.EntityConfig.Portaria;
using s4t.Erp.Graos.Data.EntityConfig.RecepcaoExpedicao;
using s4t.Erp.Graos.Domain.Armazenagem.Entities;
using s4t.Erp.Graos.Domain.Armazenagem.Enums;
using s4t.Erp.Graos.Domain.Armazenagem.ValueObjects;
using s4t.Erp.Graos.Domain.Balanca.Entities;
using s4t.Erp.Graos.Domain.Nucleo.Entities;
using s4t.Erp.Graos.Domain.Portaria.Entities;
using s4t.Erp.Graos.Domain.RecepcaoExpedicao.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace s4t.Erp.Graos.Data.Context
{
    public class GraosContext : DbContext
    {
        //Gr�os N�cleo
        public DbSet<NotaFiscalGraos> NotasFiscaisGraos { get; set; }
        public DbSet<NotaFiscalGraosCertificado> NotasFiscaisGraosCertificados { get; set; }
        public DbSet<TicketPortaria> TicketsPortaria { get; set; }
        public DbSet<Embalagem> Embalagens { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<LoteEmbalagemNumeracao> LoteEmbalagemNumeracoes { get; set; }

        //Gr�os Portaria
        public DbSet<RegistroDePortaria> RegistrosDePortaria { get; set; }
        public DbSet<RegistroDePortariaServicoAvulso> RegistrosDePortariaServicosAvulsos { get; set; }
        public DbSet<ProdutoPortaria> ProdutosPortaria { get; set; }
        public DbSet<RegistroDePortariaLog> ProdutosPortariaLog { get; set; }

        //Gr�os Recep��o Expedi��o
        public DbSet<DocumentoEntrada> DocumentosEntrada { get; set; }
        public DbSet<DocumentoEntradaEmbalagem> DocumentoEntradaEmbalagens { get; set; }

        //Gr�os Balan�a
        public DbSet<TicketPesagem> TicketsPesagem { get; set; }
        public DbSet<TicketPesagemMovimentacao> TicketPesagemMovimentacoes { get; set; }

        //Gr�os Armazenagem
        public DbSet<Armazem> Armazens { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Pilha> Pilhas { get; set; }
        public DbSet<Boletim> Boletins { get; set; }

        public GraosContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GraosContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("s4tGraos");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //** CONFIGURATIONS
            //Gr�os N�cleo
            modelBuilder.Configurations.Add(new NotaFiscalGraosConfig());
            modelBuilder.Configurations.Add(new LoteConfig());

            //Gr�os Portaria
            modelBuilder.Configurations.Add(new RegistroDePortariaConfig());

            //Gr�os Recep��o Expedi��o
            modelBuilder.Configurations.Add(new DocumentoEntradaConfig());
            modelBuilder.Configurations.Add(new DocumentoEntradaEmbalagemConfig());

            //Gr�os Balan�a
            modelBuilder.Configurations.Add(new TicketPesagemConfig());
            modelBuilder.Configurations.Add(new TicketPesagemMovimentacaoConfig());


            //** VALUE OBJECTS COMPLEX TYPES CONFIG
            //Gr�os N�cleo

            //Gr�os Portaria

            //Gr�os Recep��o Expedi��o
            modelBuilder.ComplexType<BoletimDocumento>()
                .Ignore(b => b.DocumentoEntrada);

            //Gr�os Balan�a

            //Gr�os Armazenagem
            modelBuilder.ComplexType<BoletimSerie>()
                .Ignore(r => r.Name);

            //** IGNORES
            //Gr�os N�cleo
            modelBuilder.ComplexType<Localizacao>()
                .Ignore(l => l.Armazem)
                .Ignore(l => l.Local)
                .Ignore(l => l.Pilha);

            //--- Compartilhados com Cadastro
            modelBuilder.Ignore<Motorista>();
            modelBuilder.Ignore<FornecedorGraos>();

            //Gr�os Portaria

            //Gr�os Recep��o Expedi��o

            //Gr�os Balan�a

            base.OnModelCreating(modelBuilder);
        }
    }
}
