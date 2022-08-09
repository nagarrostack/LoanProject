using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Loans.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TypeCatalog> TypeCatalogs { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientPhone> ClientPhones { get; set; }
        public DbSet<ClientBusinessInfo> ClientBusinessInfos { get; set; }
        public DbSet<ClientLoan> ClientLoans { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
