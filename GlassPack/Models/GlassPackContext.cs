using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GlassPack.Models
{
    class GlassPackContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public GlassPackContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GlassPack.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Brand>().HasData(
                   new Brand
                   {
                       Id = 1,
                       Title = "Нет бренда"
                   }
           );

            modelBuilder.Entity<Provider>().HasData(
                   new Provider
                   {
                       Id = 1,
                       Title = "Нет поставщика"
                   }
           );

            modelBuilder.Entity<Warehouse>().HasData(
                  new Warehouse
                  {
                      Id = 1,
                      Title = "Основной"
                  }
          );
        }
    }
}