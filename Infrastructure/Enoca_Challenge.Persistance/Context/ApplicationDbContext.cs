using Enoca_Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enoca_Challenge.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Carrier> Carriers { get; set; }

        public DbSet<CarrierConfiguration> CarriersConfigurations { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CarrierReport> CarrierReports { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Carrier>(entity =>
            {
                modelBuilder.Entity<CarrierConfiguration>()
                  .Property(c => c.CarrierCost)
                  .HasColumnType("decimal(18, 2)"); 

                modelBuilder.Entity<Order>()
                    .Property(o => o.OrderCarrierCost)
                    .HasColumnType("decimal(18, 2)");

                entity.HasKey(c => c.Id);

                entity.HasMany(c => c.CarrierConfigurations)
                      .WithOne(a => a.Carrier)
                      .OnDelete(DeleteBehavior.Restrict);//mevcut CarrierConfigurationlar silinmesin

                entity.HasMany(c => c.Orders)
                      .WithOne(o => o.Carrier)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
