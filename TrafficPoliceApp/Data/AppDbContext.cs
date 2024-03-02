using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficPoliceApp.Models;

namespace Turbo.az.Data;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Logging> Logs { get; set; }
    public DbSet<Fine> Fines { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Logging>().HasKey(l => l.LogId);
    }
}