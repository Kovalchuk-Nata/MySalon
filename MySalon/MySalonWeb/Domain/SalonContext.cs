using Microsoft.EntityFrameworkCore;
using MySalonWeb.Models;

namespace MySalonWeb.Domain
{
    public class SalonContext : DbContext
    {
        
        public SalonContext(DbContextOptions<SalonContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; } = default;
        public DbSet<Expert> Experts { get; set; } = default;
        public DbSet<Order> Orders { get; set; } = default;
        public DbSet<Client> Clients { get; set; } = default;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Service>().HasData(
        //            new Service { Id = 1, ServiceType = "Hair Cuts and Style", ServiceName = "Women's Haircut", Price = 37 }
        //    );
        //}
    }
}
