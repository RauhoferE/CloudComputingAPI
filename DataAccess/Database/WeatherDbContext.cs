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

                x.HasData(
                    new City { Id = 1, Name = "New York", RegionId = 1 },
                    new City { Id = 2, Name = "Philadelphia", RegionId = 1 },
                    new City { Id = 3, Name = "Washington D.C.", RegionId = 1 },
                    new City { Id = 4, Name = "Los Angeles", RegionId = 2 },
                    new City { Id = 5, Name = "San Francisco", RegionId = 2 },
                    new City { Id = 6, Name = "Las Vegas", RegionId = 2 },
                    new City { Id = 7, Name = "London", RegionId = 3 },
                    new City { Id = 8, Name = "Paris", RegionId = 3 },
                    new City { Id = 9, Name = "Vienna", RegionId = 3 },
                    new City { Id = 10, Name = "Tokyo", RegionId = 4 },
                    new City { Id = 11, Name = "Osaka", RegionId = 4 },
                    new City { Id = 12, Name = "Busan", RegionId = 4 }
                    );

            });

            modelBuilder.Entity<Region>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasIndex(y => y.Name)
                 .IsUnique();
                x.HasMany(y => y.Cities)
                 .WithOne(z => z.Region)
                 .HasForeignKey(z => z.RegionId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasData(
                    new Region { Id = 1, Name = "US-East" },
                    new Region { Id = 2, Name = "US-West" },
                    new Region { Id = 3, Name = "Europe" },
                    new Region { Id = 4, Name = "Asia" });
            });

            modelBuilder.Entity<Condition>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasIndex(y => y.Name)
                    .IsUnique();
                x.HasMany(y => y.WeatherData)
                 .WithOne(z => z.Condition)
                 .HasForeignKey(z => z.ConditionId)
                 .OnDelete(DeleteBehavior.Cascade); 
                x.HasData(
                    new Condition { Id = 1, Name = "Sunny", Description="Sunny weather" },
                    new Condition { Id = 2, Name = "Cloudy", Description = "Cloudy weather" },
                    new Condition { Id = 3, Name = "Rainy", Description = "Rainy weather" },
                    new Condition { Id = 4, Name = "Stormy", Description = "Stormy weather" },
                    new Condition { Id = 5, Name = "Snowy", Description = "Snowy weather" }
                );
            });

            modelBuilder.Entity<WeatherData>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasOne(y => y.City)
                 .WithMany(z => z.WeatherData)
                 .OnDelete(DeleteBehavior.Cascade);
                x.HasIndex(y => new { y.CityId, y.Date })
                 .IsUnique();

                x.HasData(
    // --- USA EAST COAST (RegionId = 1) ---
    // New York (CityId = 1)
    new WeatherData { Id = 1, CityId = 1, Date = new DateTime(2025, 7, 15, 14, 0, 0), TemperatureCelsius = 28.5f, HumidityPercent = 65.0f, WindSpeedKph = 12.5f, ConditionId = 1 }, // Sunny Summer Day
    new WeatherData { Id = 2, CityId = 1, Date = new DateTime(2025, 4, 10, 8, 30, 0), TemperatureCelsius = 15.2f, HumidityPercent = 88.0f, WindSpeedKph = 8.0f, ConditionId = 3 }, // Rainy Spring Morning
    new WeatherData { Id = 3, CityId = 1, Date = new DateTime(2025, 1, 5, 10, 0, 0), TemperatureCelsius = -1.0f, HumidityPercent = 75.0f, WindSpeedKph = 20.0f, ConditionId = 5 }, // Snowy Winter Day

    // Philadelphia (CityId = 2)
    new WeatherData { Id = 4, CityId = 2, Date = new DateTime(2025, 8, 20, 15, 0, 0), TemperatureCelsius = 31.0f, HumidityPercent = 60.0f, WindSpeedKph = 15.0f, ConditionId = 1 },
    new WeatherData { Id = 5, CityId = 2, Date = new DateTime(2025, 5, 1, 12, 0, 0), TemperatureCelsius = 18.5f, HumidityPercent = 70.0f, WindSpeedKph = 10.0f, ConditionId = 2 },
    new WeatherData { Id = 6, CityId = 2, Date = new DateTime(2025, 9, 25, 17, 30, 0), TemperatureCelsius = 16.0f, HumidityPercent = 90.0f, WindSpeedKph = 25.0f, ConditionId = 4 }, // Stormy Autumn

    // Washington D.C. (CityId = 3)
    new WeatherData { Id = 7, CityId = 3, Date = new DateTime(2025, 6, 1, 13, 0, 0), TemperatureCelsius = 26.0f, HumidityPercent = 55.0f, WindSpeedKph = 11.0f, ConditionId = 1 },
    new WeatherData { Id = 8, CityId = 3, Date = new DateTime(2025, 12, 12, 9, 0, 0), TemperatureCelsius = 4.5f, HumidityPercent = 80.0f, WindSpeedKph = 5.0f, ConditionId = 2 },
    new WeatherData { Id = 9, CityId = 3, Date = new DateTime(2025, 3, 18, 11, 0, 0), TemperatureCelsius = 8.2f, HumidityPercent = 92.0f, WindSpeedKph = 18.0f, ConditionId = 3 },

    // --- USA WEST COAST (RegionId = 2) ---
    // Los Angeles (CityId = 4)
    new WeatherData { Id = 10, CityId = 4, Date = new DateTime(2025, 9, 1, 14, 0, 0), TemperatureCelsius = 32.0f, HumidityPercent = 35.0f, WindSpeedKph = 5.0f, ConditionId = 1 }, // Hot, Dry Summer
    new WeatherData { Id = 11, CityId = 4, Date = new DateTime(2025, 11, 15, 10, 0, 0), TemperatureCelsius = 21.0f, HumidityPercent = 50.0f, WindSpeedKph = 10.0f, ConditionId = 2 },
    new WeatherData { Id = 12, CityId = 4, Date = new DateTime(2025, 2, 28, 16, 0, 0), TemperatureCelsius = 17.5f, HumidityPercent = 75.0f, WindSpeedKph = 8.5f, ConditionId = 3 },

    // San Francisco (CityId = 5)
    new WeatherData { Id = 13, CityId = 5, Date = new DateTime(2025, 7, 20, 11, 0, 0), TemperatureCelsius = 19.5f, HumidityPercent = 78.0f, WindSpeedKph = 30.0f, ConditionId = 2 }, // Foggy/Cloudy Day
    new WeatherData { Id = 14, CityId = 5, Date = new DateTime(2025, 10, 1, 14, 0, 0), TemperatureCelsius = 24.0f, HumidityPercent = 60.0f, WindSpeedKph = 15.0f, ConditionId = 1 },
    new WeatherData { Id = 15, CityId = 5, Date = new DateTime(2025, 3, 5, 18, 0, 0), TemperatureCelsius = 13.0f, HumidityPercent = 85.0f, WindSpeedKph = 18.0f, ConditionId = 3 },

    // Las Vegas (CityId = 6)
    new WeatherData { Id = 16, CityId = 6, Date = new DateTime(2025, 8, 5, 15, 0, 0), TemperatureCelsius = 40.5f, HumidityPercent = 15.0f, WindSpeedKph = 10.0f, ConditionId = 1 }, // Extremely Hot, Dry
    new WeatherData { Id = 17, CityId = 6, Date = new DateTime(2025, 1, 1, 12, 0, 0), TemperatureCelsius = 14.0f, HumidityPercent = 30.0f, WindSpeedKph = 5.0f, ConditionId = 1 },
    new WeatherData { Id = 18, CityId = 6, Date = new DateTime(2025, 12, 24, 8, 0, 0), TemperatureCelsius = 8.8f, HumidityPercent = 45.0f, WindSpeedKph = 12.0f, ConditionId = 2 },

    // --- EUROPE (RegionId = 3) ---
    // London (CityId = 7)
    new WeatherData { Id = 19, CityId = 7, Date = new DateTime(2025, 6, 15, 11, 0, 0), TemperatureCelsius = 18.0f, HumidityPercent = 75.0f, WindSpeedKph = 10.0f, ConditionId = 2 },
    new WeatherData { Id = 20, CityId = 7, Date = new DateTime(2025, 10, 5, 16, 0, 0), TemperatureCelsius = 12.5f, HumidityPercent = 90.0f, WindSpeedKph = 15.0f, ConditionId = 3 }, // Typical London Rain
    new WeatherData { Id = 21, CityId = 7, Date = new DateTime(2025, 7, 28, 14, 0, 0), TemperatureCelsius = 25.5f, HumidityPercent = 55.0f, WindSpeedKph = 8.0f, ConditionId = 1 },

    // Paris (CityId = 8)
    new WeatherData { Id = 22, CityId = 8, Date = new DateTime(2025, 8, 1, 13, 0, 0), TemperatureCelsius = 30.0f, HumidityPercent = 50.0f, WindSpeedKph = 10.0f, ConditionId = 1 },
    new WeatherData { Id = 23, CityId = 8, Date = new DateTime(2025, 4, 20, 10, 0, 0), TemperatureCelsius = 14.0f, HumidityPercent = 65.0f, WindSpeedKph = 18.0f, ConditionId = 3 },
    new WeatherData { Id = 24, CityId = 8, Date = new DateTime(2025, 11, 1, 17, 0, 0), TemperatureCelsius = 9.5f, HumidityPercent = 75.0f, WindSpeedKph = 5.0f, ConditionId = 2 },

    // Vienna (CityId = 9)
    new WeatherData { Id = 25, CityId = 9, Date = new DateTime(2025, 9, 10, 12, 0, 0), TemperatureCelsius = 22.0f, HumidityPercent = 60.0f, WindSpeedKph = 15.0f, ConditionId = 1 },
    new WeatherData { Id = 26, CityId = 9, Date = new DateTime(2025, 2, 1, 7, 0, 0), TemperatureCelsius = 2.0f, HumidityPercent = 80.0f, WindSpeedKph = 5.0f, ConditionId = 5 }, // Snowy Morning
    new WeatherData { Id = 27, CityId = 9, Date = new DateTime(2025, 7, 5, 16, 0, 0), TemperatureCelsius = 29.5f, HumidityPercent = 45.0f, WindSpeedKph = 25.0f, ConditionId = 4 }, // Afternoon Storm

    // --- ASIA (RegionId = 4) ---
    // Tokyo (CityId = 10)
    new WeatherData { Id = 28, CityId = 10, Date = new DateTime(2025, 8, 10, 14, 0, 0), TemperatureCelsius = 33.0f, HumidityPercent = 85.0f, WindSpeedKph = 10.0f, ConditionId = 3 }, // Hot, Humid, Rainy
    new WeatherData { Id = 29, CityId = 10, Date = new DateTime(2025, 11, 25, 11, 0, 0), TemperatureCelsius = 17.5f, HumidityPercent = 60.0f, WindSpeedKph = 12.0f, ConditionId = 1 },
    new WeatherData { Id = 30, CityId = 10, Date = new DateTime(2025, 1, 15, 15, 0, 0), TemperatureCelsius = 6.0f, HumidityPercent = 70.0f, WindSpeedKph = 5.0f, ConditionId = 2 },

    // Osaka (CityId = 11)
    new WeatherData { Id = 31, CityId = 11, Date = new DateTime(2025, 7, 1, 16, 0, 0), TemperatureCelsius = 30.5f, HumidityPercent = 90.0f, WindSpeedKph = 20.0f, ConditionId = 4 }, // Summer Typhoon/Storm
    new WeatherData { Id = 32, CityId = 11, Date = new DateTime(2025, 4, 5, 9, 0, 0), TemperatureCelsius = 15.0f, HumidityPercent = 75.0f, WindSpeedKph = 8.0f, ConditionId = 2 },
    new WeatherData { Id = 33, CityId = 11, Date = new DateTime(2025, 12, 18, 13, 0, 0), TemperatureCelsius = 8.0f, HumidityPercent = 65.0f, WindSpeedKph = 10.0f, ConditionId = 1 },

    // Busan (CityId = 12)
    new WeatherData { Id = 34, CityId = 12, Date = new DateTime(2025, 6, 20, 14, 0, 0), TemperatureCelsius = 25.0f, HumidityPercent = 70.0f, WindSpeedKph = 18.0f, ConditionId = 1 }, // Coast wind
    new WeatherData { Id = 35, CityId = 12, Date = new DateTime(2025, 10, 25, 10, 0, 0), TemperatureCelsius = 17.0f, HumidityPercent = 80.0f, WindSpeedKph = 5.0f, ConditionId = 3 },
    new WeatherData { Id = 36, CityId = 12, Date = new DateTime(2025, 3, 10, 12, 0, 0), TemperatureCelsius = 9.5f, HumidityPercent = 72.0f, WindSpeedKph = 14.0f, ConditionId = 2 }
);
            });


        }
    }
}
