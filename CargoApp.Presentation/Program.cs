using CargoApp.Application.Services;
using CargoApp.Application.Services.Interfaces;
using CargoApp.Domain.Repositories.Interfaces;
using CargoApp.Infrastructure.Persistence;
using CargoApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace CargoApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<ICargoService, CargoService>();

        var connectionString = builder.Environment.IsDevelopment()
            ? builder.Configuration["Connection_Strings"]
            : builder.Configuration["Connection_Strings_DefaultConnection"];

        var loggerFactory = LoggerFactory.Create(loggingBuilder => { loggingBuilder.AddConsole(); });

        var logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation("Connection string: {connectionString}", connectionString);
        logger.LogInformation("IsDevelopment: {development}", builder.Environment.IsDevelopment());

        // При запуске без докера храним данные в памяти приложения 
        if (string.IsNullOrEmpty(connectionString))
        {
            logger.LogWarning("The connection string is empty. The data will be stored in the application's memory");
            builder.Services.AddSingleton<ICargoRepository, CargoTestRepository>();
        }
        // Иначе подключемся к базе данных
        else
        {
            logger.LogInformation("Connecting to the database.");

            // Проверка валидности подключения
            if (IsDatabaseConnectionValid(logger, connectionString))
            {
                builder.Services.AddDbContext<CargoDbContext>(options => options.UseNpgsql(connectionString));
                builder.Services.AddScoped<ICargoRepository, CargoRepository>();
            }
            else
            {
                logger.LogError("Database connection is not valid. Using in-memory storage instead.");
                builder.Services.AddSingleton<ICargoRepository, CargoTestRepository>();
            }
        }

        var app = builder.Build();

        // Инициализируем данные
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var cargoRepository = scope.ServiceProvider.GetRequiredService<ICargoRepository>();
        var dataInitializer = new DataInitializer(cargoRepository);
        dataInitializer.InitializeData();

        if (!app.Environment.IsDevelopment())
        {
            //app.UseExceptionHandler("/Cargo/NotFound");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePages();
        app.UseExceptionHandler("/Сargo/NotFound");
        app.UseStatusCodePagesWithReExecute("/Cargo/NotFound", "?code={0}");

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Cargo}/{action=Index}/{id?}");

        app.Run();
    }

    // Метод для проверки валидности подключения к базе данных
    private static bool IsDatabaseConnectionValid(ILogger logger, string connectionString)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during database connection validation.");
            return false;
        }
    }
}