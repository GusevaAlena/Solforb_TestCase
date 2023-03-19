using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

public class DatabaseContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.Migrate();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasData(
                new Provider { Id = 1, Name = "Провайдер1" },
                new Provider { Id = 2, Name = "Провайдер2" },
                new Provider { Id = 3, Name = "Провайдер3" },
                new Provider { Id = 4, Name = "Провайдер4" },
                new Provider { Id = 5, Name = "Провайдер5" },
                new Provider { Id = 6, Name = "Провайдер6" }
                );
        });
    }
}
