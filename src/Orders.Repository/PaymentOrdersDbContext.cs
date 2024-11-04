using Microsoft.EntityFrameworkCore;
using Orders.Repository.Models;
using System.Reflection;

namespace Orders.Repository
{
    /// <summary>
    /// Bd context is the object to represents the database integration.
    /// </summary>
    public class PaymentOrdersDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentOrdersDbContext"/> class.
        /// <see cref="PaymentOrdersDbContext"/> Constructor.
        /// </summary>
        /// <param name="options">Dependency injection options to set up the possibles configurations.</param>
        public PaymentOrdersDbContext(DbContextOptions<PaymentOrdersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Provider>().HasData(
                new Provider { Id = Guid.Parse("1F2DD793-E8D4-40D8-B552-9C04AE8CF266"), Name = "PagaFacil", IsActive = true },
                new Provider { Id = Guid.Parse("A593DDF5-A168-425C-8E30-AC1AD434195C"), Name = "CazaPagos", IsActive = true },
                new Provider { Id = Guid.Parse("CC981E65-4C3E-44CF-82DB-88B8DC9114B4"), Name = "Transfer", IsActive = true });

            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType { Id = Guid.Parse("C4A400C3-439B-4CFB-8B41-9E0670A5B801"), Name = "Cash" },
                new PaymentType { Id = Guid.Parse("3DC0E447-633B-4804-B629-D67BB63318DC"), Name = "CreditCard" },
                new PaymentType { Id = Guid.Parse("3E392346-71DE-4966-B4B3-B3428B9FFE72"), Name = "Transfer" });
        }
    }
}