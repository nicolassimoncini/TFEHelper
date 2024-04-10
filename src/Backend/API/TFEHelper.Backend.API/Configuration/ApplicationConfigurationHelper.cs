using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Serilog;
using TFEHelper.Backend.Infrastructure.Database.Implementations;

namespace TFEHelper.Backend.API.Configuration
{
    public static class ApplicationConfigurationHelper
    {
        // Para generar la migración:
        //      - add-migration InitialCreate -OutputDir Database\Migrations -Context ApplicationDbContext -Project TFEHelper.Backend.Infrastructure -StartupProject TFEHelper.Backend.API
        public static void ApplyDatabaseMigration(this IApplicationBuilder app)
        {
            Log.Information("Checking and aplying pending database migrations...");

            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    dataContext.Database.Migrate();
                }
            }
        }
    }
}
