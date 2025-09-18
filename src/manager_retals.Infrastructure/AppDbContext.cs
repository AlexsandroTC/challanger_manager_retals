using manager_retals.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace manager_retals.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Motorcycle> Motorcycles;
        public DbSet<Driver> Drivers;
        public DbSet<Rental> Rentals;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyMotorcycleMapping(modelBuilder);
            ApplyDriveMapping(modelBuilder);
            ApplyRentalMapping(modelBuilder);
        }

        private static void ApplyRentalMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                        .HasKey(r => r.Id);

            modelBuilder.Entity<Rental>()
                        .HasOne(r => r.Motorcycle)
                        .WithMany(m => m.Rentals)
                        .HasForeignKey(r => r.MotorcycleId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                        .HasOne(r => r.Driver)
                        .WithMany(d => d.Rentals)
                        .HasForeignKey(r => r.DriverId)
                        .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ApplyDriveMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                        .HasKey(d => d.Id);

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.Document)
                        .IsUnique();
        }

        private static void ApplyMotorcycleMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>()
                        .HasKey(m => m.Id);

            modelBuilder.Entity<Motorcycle>()
                        .HasIndex(m => m.Identifier)
                        .IsUnique();

            modelBuilder.Entity<Motorcycle>()
                        .HasIndex(m => m.Plate)
                        .IsUnique();
        }
    }
}
