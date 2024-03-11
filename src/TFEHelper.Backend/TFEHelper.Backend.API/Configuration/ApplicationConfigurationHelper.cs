using Microsoft.EntityFrameworkCore;
using Serilog;
using TFEHelper.Backend.Infrastructure.Database.Implementations;

namespace TFEHelper.API.Configuration
{
    public static class ApplicationConfigurationHelper
    {
        // Para generar la migración:
        //      - add-migration InitialCreate -OutputDir Database\Migrations -Context ApplicationDbContext -Project TFEHelper.Backend.Infrastructure -StartupProject TFEHelper.API
        public static void ApplyDatabaseMigration(this IApplicationBuilder app)
        {
            Log.Information("Checking and aplying pending database migrations...");
            using var scope = app.ApplicationServices.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
