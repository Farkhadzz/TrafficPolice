using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Data;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Logging> Logs { get; set; }
    public DbSet<Fine> Fines { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public MyDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Logging>().HasKey(l => l.LogId);
    }
}