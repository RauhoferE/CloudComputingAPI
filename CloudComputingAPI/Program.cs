
using AutoMapper;
using CloudComputingAPI.Interfaces;
using CloudComputingAPI.MapperProfiles;
using CloudComputingAPI.Middleware;
using CloudComputingAPI.Services;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CloudComputingAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration.Build())
                .CreateLogger();

            // Used for serilog request logging
            Log.Logger = logger;

            // Add services to the container.
            builder.Services.AddSerilog();

            builder.Services.AddControllers(x =>
            {
                x.Filters.Add<HttpResponseExceptionFilter>();
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("WeatherApiCors",
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration.GetSection("AllowedCorsHosts").Get<string[]>())
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            var env = builder.Configuration["WeatherDbConnectionEnv"];

            if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<WeatherDbContext>(x =>
                    x.UseSqlServer(builder.Configuration["WeatherDbConnectionEnv"]));
            }
            else
            {
                builder.Services.AddDbContext<WeatherDbContext>(x =>
                    x.UseSqlServer(builder.Configuration.GetConnectionString("WeatherDbConnection")));
            }

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddAutoMapper(x =>
            {
                x.AddProfile<EntityToDtoMapper>();
            });

            builder.Services.AddTransient<IWeatherService, WeatherService>();

            var app = builder.Build();
            await EnsureDatabaseIsCreatedAndMigrated(app);

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("WeatherApiCors");

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.MapFallbackToFile("index.html");

            try
            {
                app.Run();
                logger.Information("Application started.");
            }
            catch (Exception e)
            {
                logger.Fatal("Application terminated unexpectedly: ");
                logger.Fatal(e.Message);
            }
            
        }

        private static async Task EnsureDatabaseIsCreatedAndMigrated(IHost app)
        {
            // Scopes are created for transient operations
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Resolve the DbContext instance
                    var context = services.GetRequiredService<WeatherDbContext>();
                    var logger = services.GetRequiredService<Serilog.ILogger>();

                    logger.Information("Migrating database.....");

                    // Apply any pending migrations or create the database if it doesn't exist
                    await context.Database.MigrateAsync();

                    logger.Information("Migrating database done.");
                    // Optional: You can also add seed data logic here if needed:
                    // await SeedData.Initialize(context); 
                }
                catch (Exception ex)
                {
                    // Log any errors that occurred during migration
                    var logger = services.GetRequiredService<Serilog.ILogger>();
                    logger.Error(ex, "An error occurred while migrating or seeding the database.");

                    // Note: In production, you might want to stop the application here if DB is critical
                    // throw; 
                }
            }
        }
    }
}
