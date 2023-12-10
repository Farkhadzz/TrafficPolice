using Microsoft.EntityFrameworkCore;
using TrafficPoliceMVC.Models;

namespace TrafficPoliceMVC.Sql;
    public class AppDbContext : DbContext
    {
        private readonly string connectionString;

        public AppDbContext()
        {
            // Здесь укажите вашу строку подключения
            this.connectionString = "Server=(localdb)\\mssqllocaldb;Database=TrafficPoliceDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public DbSet<FineModel> Fines { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }