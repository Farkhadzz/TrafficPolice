using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Data;

public class MyDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Logging> Logs { get; set; }
    public DbSet<Fine> Fines { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public MyDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Logging>().HasKey(l => l.LogId);
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }

}