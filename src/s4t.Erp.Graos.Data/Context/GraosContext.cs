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
        //Grãos Núcleo
        public DbSet<NotaFiscalGraos> NotasFiscaisGraos { get; set; }
        public DbSet<NotaFiscalGraosCertificado> NotasFiscaisGraosCertificados { get; set; }
        public DbSet<TicketPortaria> TicketsPortaria { get; set; }
        public DbSet<Embalagem> Embalagens { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<LoteEmbalagemNumeracao> LoteEmbalagemNumeracoes { get; set; }

        //Grãos Portaria
        public DbSet<RegistroDePortaria> RegistrosDePortaria { get; set; }
        public DbSet<RegistroDePortariaServicoAvulso> RegistrosDePortariaServicosAvulsos { get; set; }
        public DbSet<ProdutoPortaria> ProdutosPortaria { get; set; }
        public DbSet<RegistroDePortariaLog> ProdutosPortariaLog { get; set; }

        //Grãos Recepção Expedição
        public DbSet<DocumentoEntrada> DocumentosEntrada { get; set; }
        public DbSet<DocumentoEntradaEmbalagem> DocumentoEntradaEmbalagens { get; set; }

        //Grãos Balança
        public DbSet<TicketPesagem> TicketsPesagem { get; set; }
        public DbSet<TicketPesagemMovimentacao> TicketPesagemMovimentacoes { get; set; }

        //Grãos Armazenagem
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
            //Grãos Núcleo
            modelBuilder.Configurations.Add(new NotaFiscalGraosConfig());
            modelBuilder.Configurations.Add(new LoteConfig());

            //Grãos Portaria
            modelBuilder.Configurations.Add(new RegistroDePortariaConfig());

            //Grãos Recepção Expedição
            modelBuilder.Configurations.Add(new DocumentoEntradaConfig());
            modelBuilder.Configurations.Add(new DocumentoEntradaEmbalagemConfig());

            //Grãos Balança
            modelBuilder.Configurations.Add(new TicketPesagemConfig());
            modelBuilder.Configurations.Add(new TicketPesagemMovimentacaoConfig());


            //** VALUE OBJECTS COMPLEX TYPES CONFIG
            //Grãos Núcleo

            //Grãos Portaria

            //Grãos Recepção Expedição
            modelBuilder.ComplexType<BoletimDocumento>()
                .Ignore(b => b.DocumentoEntrada);

            //Grãos Balança

            //Grãos Armazenagem
            modelBuilder.ComplexType<BoletimSerie>()
                .Ignore(r => r.Name);

            //** IGNORES
            //Grãos Núcleo
            modelBuilder.ComplexType<Localizacao>()
                .Ignore(l => l.Armazem)
                .Ignore(l => l.Local)
                .Ignore(l => l.Pilha);

            //--- Compartilhados com Cadastro
            modelBuilder.Ignore<Motorista>();
            modelBuilder.Ignore<FornecedorGraos>();

            //Grãos Portaria

            //Grãos Recepção Expedição

            //Grãos Balança

            base.OnModelCreating(modelBuilder);
        }
    }
}
