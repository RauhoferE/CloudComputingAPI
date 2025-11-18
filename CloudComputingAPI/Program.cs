
using CloudComputingAPI.Interfaces;
using CloudComputingAPI.Services;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CloudComputingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.Host.UseSerilog((_, config) =>
            {
                config.ReadFrom.Configuration(builder.Configuration);
            });


            // Add services to the container.
            builder.Services.AddSerilog();

            builder.Services.AddControllers();

            builder.Services.AddDbContext<WeatherDbContext>(x => 
            x.UseSqlServer(builder.Configuration.GetConnectionString("WeatherDbConnection")));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddTransient<IWeatherService, WeatherService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSerilogRequestLogging();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
