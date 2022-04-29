using Berenice.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Berenice.Infrastructure.Data
{
    public partial class BereniceDBContext : DbContext
    {
        public BereniceDBContext()
        {
        }

        public BereniceDBContext(DbContextOptions<BereniceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BereniceDBContext).Assembly);
        }
    }
}
