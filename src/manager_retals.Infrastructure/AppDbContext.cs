using manager_retals.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace manager_retals.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyMotorcycleMapping(modelBuilder);
            ApplyDriverMapping(modelBuilder);
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

        private static void ApplyDriverMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                        .HasKey(d => d.Id);

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.Identifier)
                        .IsUnique();

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.Name);

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.CompanyNumber)
                        .IsUnique();

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.BirthDate);

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.DriverLicenseNumber)
                        .IsUnique();

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.DriverLicenseType);

            modelBuilder.Entity<Driver>()
                        .HasIndex(d => d.DriverLicenseImagePath);
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
