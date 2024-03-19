using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TFEHelper.Backend.API.Configuration;
using TFEHelper.Backend.API.Middleware;
using TFEHelper.Backend.Core.Engine.Implementations;
using TFEHelper.Backend.Core.Engine.Interfaces;
using TFEHelper.Backend.Core.Plugin.Implementations;
using TFEHelper.Backend.Core.Plugin.Interfaces;
using TFEHelper.Backend.Domain.Config;
using TFEHelper.Backend.Infrastructure.Database.Implementations;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;
using TFEHelper.Backend.Tools.Strings;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        SetupStaticLogger(args);
        try 
        {
            Log.Information("Starting web application for TFEHelper.");

            var webApp = await CreateWebApplication(args);
            webApp.Run();

            return 0;
        } 
        catch (Exception ex) 
        {
            if (ex is not OperationCanceledException)
            {
                Log.Fatal(ex, "Web application for TFEHelper terminated unexpectedly.");
                return 1;
            }
            else return 0;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static void SetupStaticLogger(string[] args)
    {
        var environmentCurrent = CommandLineHelper.GetArgument(args, "--environment");
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentCurrent ?? "Production"}.json", optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static async Task<WebApplication> CreateWebApplication(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog();

        builder.Services.AddControllers(option =>
        {
            option.CacheProfiles.Add("Default30",
                new CacheProfile()
                {
                    Duration = 30
                });
        }).AddNewtonsoftJson();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddResponseCaching();

        builder.Services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlite(builder.Configuration.GetValue<string>("Database:ConnectionString"), 
                m => m.MigrationsAssembly(builder.Configuration.GetValue<string>("Database:MigrationAssembly")));
        });

        builder.Services.AddAutoMapper(typeof(MappingConfig));

        builder.Services.AddScoped<IRepository, Repository>();
        builder.Services.AddSingleton<IPluginManager, PluginManager>();    
        builder.Services.AddScoped<ITFEHelperEngine, TFEHelperEngine>();

        var app = builder.Build();

        var pluginManager = app.Services.GetService<IPluginManager>();
        if (pluginManager != null) await pluginManager.ScanAsync();

        app.UseResponseCaching();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.MapControllers();
        app.ApplyDatabaseMigration();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}