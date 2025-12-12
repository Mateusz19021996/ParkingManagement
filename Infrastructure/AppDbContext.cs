using Microsoft.EntityFrameworkCore;
using ParkingManagement.src.domain.Entities;

namespace ParkingManagement.src.infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingSpace>()
                .HasOne(p => p.Vehicle)
                .WithOne()
                .HasForeignKey<ParkingSpace>(p => p.VehicleId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("ParkingManagementDb");
            }
        }
    }
}
