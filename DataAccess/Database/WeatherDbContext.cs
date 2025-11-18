using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Database
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options): base(options)
        {
            
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public DbSet<WeatherData> WeatherDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasIndex(y => y.Name)
                 .IsUnique();
                x.HasOne(y => y.Region)
                 .WithMany(z => z.Cities)
                 .OnDelete(DeleteBehavior.Cascade);
                x.HasMany(y => y.WeatherData)
                 .WithOne(z => z.City)
                 .HasForeignKey(z => z.CityId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Region>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasIndex(y => y.Name)
                 .IsUnique();
                x.HasMany(y => y.Cities)
                 .WithOne(z => z.Region)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Condition>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasIndex(y => y.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<WeatherData>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasOne(y => y.City)
                 .WithMany(z => z.WeatherData)
                 .OnDelete(DeleteBehavior.Cascade);
                x.HasIndex(y => new { y.CityId, y.Date })
                 .IsUnique();
            });


        }
    }
}
