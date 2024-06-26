﻿using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TFEHelper.Backend.API.Configuration;
using TFEHelper.Backend.API.Controllers;
using TFEHelper.Backend.API.Filters;
using TFEHelper.Backend.API.Middleware;
using TFEHelper.Backend.Infrastructure.Database.Implementations;
using TFEHelper.Backend.Services.Common;
using TFEHelper.Backend.Services.Implementations;
using TFEHelper.Backend.Services.Implementations.Business;
using TFEHelper.Backend.Services.Implementations.Configuration;
using TFEHelper.Backend.Services.Implementations.Plugin;
using TFEHelper.Backend.Tools.Logging;
using TFEHelper.Backend.Tools.Strings;

namespace TFEHelper.Backend.API
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            SetupStaticLogger(args);
            try
            {
                Log.Information("Starting web API for TFEHelper backend.");

                using (var cts = new CancellationTokenSource())
                {
                    var webApp = await CreateWebApplication(args, cts.Token);
                    await webApp.RunAsync(cts.Token);
                }
                return 0;
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Log.Fatal(ex, "Web API for TFEHelper backend terminated unexpectedly.");
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

        private static async Task<WebApplication> CreateWebApplication(string[] args, CancellationToken cancellationToken = default)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            builder.Services.AddControllers(option =>
            {
                option.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            }).AddNewtonsoftJson();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => 
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "TFEHelper API",
                        License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") },
                        Version = "v1",
                        Description = "API access to TFEHelper Backend.",
                        Contact = new OpenApiContact() { Name = "Nicolás Simoncini", Email = "nicolassimoncini@hotmail.com", Url = new Uri("https://github.com/nicolassimoncini/TFEHelper") }
                    });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                    c.ExampleFilters();
                    c.EnableAnnotations();
                });
            builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
            builder.Services.AddResponseCaching();

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlite(builder.Configuration.GetValue<string>("Database:SQLite:ConnectionString"),
                    m => m.MigrationsAssembly(builder.Configuration.GetValue<string>("Database:SQLite:MigrationAssembly")));
            });

            builder.Services.AddAutoMapper(
                profileAssemblyMarkerTypes: typeof(MappingConfig),
                configAction: cfg => cfg.AddExpressionMapping());

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddSingleton(typeof(ILogger<>), typeof(LoggerEx<>));

            builder.Services.AddScoped<Services.Abstractions.Interfaces.IServiceManager, ServiceManager>();
            builder.Services.AddScoped<Services.Abstractions.Interfaces.IConfigurationService, ConfigurationService>();
            builder.Services.AddScoped<Services.Abstractions.Interfaces.IPluginService, PluginService>();
            builder.Services.AddScoped<Services.Abstractions.Interfaces.IPublicationService, PublicationService>();
            builder.Services.AddSingleton<IPluginManager, PluginManager>();
            builder.Services.AddScoped<Domain.Repositories.IRepositoryManager, TFEHelper.Backend.Infrastructure.Database.Implementations.RepositoryManager>();
            builder.Services.AddScoped<Domain.Repositories.IPublicationRepository, TFEHelper.Backend.Infrastructure.Database.Implementations.PublicationRepository>();
            builder.Services.AddScoped<ValidationFilterAttribute>();

            builder.Services.AddHealthChecks();
            
            builder.Services.Configure<ApiBehaviorOptions>(options
                => options.SuppressModelStateInvalidFilter = true);

            var app = builder.Build();

            app.MapHealthChecks("/healthcheck");

            var pluginManager = app.Services.GetService<IPluginManager>();
            if (pluginManager != null) await pluginManager.ScanAsync(cancellationToken);

            app.UseExceptionHandler(opt => { });
            app.UseCors();
            app.UseResponseCaching();
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseHttpsRedirection();
            app.MapControllers();                     
            app.ApplyDatabaseMigration();

            return app;
        }
    }
}