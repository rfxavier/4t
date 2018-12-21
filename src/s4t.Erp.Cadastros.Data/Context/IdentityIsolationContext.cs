using System.Data.Entity;

namespace s4t.Erp.Cadastros.Data.Context
{
    public class IdentityIsolationContext : DbContext
    {
        public IdentityIsolationContext() 
            : base("DefaultConnection")
        {
            
        }
        public DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetUserConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}