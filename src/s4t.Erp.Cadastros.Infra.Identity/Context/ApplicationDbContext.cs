using Microsoft.AspNet.Identity.EntityFramework;
using s4t.Erp.Cadastros.Infra.Identity.Model;
using System;
using System.Data.Entity;

namespace s4t.Erp.Cadastros.Infra.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("s4tIdentity");

            base.OnModelCreating(modelBuilder);
        }
    }
}