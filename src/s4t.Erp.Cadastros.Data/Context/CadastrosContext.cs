using s4t.Erp.Cadastros.Data.EntityConfig.Nucleo.Fazendas;
using s4t.Erp.Cadastros.Data.EntityConfig.Nucleo.Nucleo;
using s4t.Erp.Cadastros.Domain.Nucleo.Entities;
using s4t.Erp.Core.Domain.Enums;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using s4t.Erp.Cadastros.Domain.Graos.Fazendas.Entities;

namespace s4t.Erp.Cadastros.Data.Context
{
    public class CadastrosContext : DbContext
    {
        //Núcleo
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Cadastro> Cadastros { get; set; }
        public DbSet<Fazenda> Fazendas { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<UF> UFs { get; set; }
        public DbSet<Pais> Paises { get; set; }

        //Núcleo Fazendas
        public DbSet<FazendaCertificacaoEmissor> FazendaCertificacaoEmissores { get; set; }
        public DbSet<FazendaCertificacao> FazendaCertificacoes { get; set; }

        public CadastrosContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CadastrosContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("s4tCadastros");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //** CONFIGURATIONS
            //Núcleo
            modelBuilder.Configurations.Add(new FilialConfig());
            modelBuilder.Configurations.Add(new CadastroConfig());

            //Núcleo Fazendas
            modelBuilder.Configurations.Add(new FazendaConfig());
            modelBuilder.Configurations.Add(new FazendaCertificacaoConfig());


            //** VALUE OBJECTS COMPLEX TYPES CONFIG
            //Núcleo
            modelBuilder.ComplexType<TipoPFPJ>()
                .Ignore(r => r.Name);

            //Núcleo Fazendas


            //** IGNORES
            //Núcleo

            //Núcleo Fazendas

            base.OnModelCreating(modelBuilder);
        }
    }
}
