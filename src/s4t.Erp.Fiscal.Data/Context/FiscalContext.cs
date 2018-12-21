using s4t.Erp.Fiscal.Data.EntityConfig;
using s4t.Erp.Fiscal.Domain.Nucleo.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace s4t.Erp.Fiscal.Data.Context
{
    public class FiscalContext : DbContext
    {
        //Fiscal
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoUnidade> ProdutoUnidades { get; set; }

        public FiscalContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FiscalContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("s4tFiscal");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //** CONFIGURATIONS
            //Fiscal
            modelBuilder.Configurations.Add(new NotaFiscalConfig());


            //** VALUE OBJECTS COMPLEX TYPES CONFIG
            //Fiscal

            //** IGNORES
            //Fiscal

            base.OnModelCreating(modelBuilder);
        }
    }
}
