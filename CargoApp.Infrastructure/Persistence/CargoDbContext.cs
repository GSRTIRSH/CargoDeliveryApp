using CargoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Infrastructure.Persistence;

public class CargoDbContext : DbContext
{
    public CargoDbContext(DbContextOptions<CargoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<Cargo>()
            .HasOne(c => c.Courier)
            .WithMany(courier => courier.CargoInDelivery)
            .HasForeignKey(c => c.CourierId);#1#
        
        base.OnModelCreating(modelBuilder);
    }*/

    public DbSet<Cargo> CargoRequests { get; set; }
    public DbSet<Courier> Couriers { get; set; }
}