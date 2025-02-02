using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Persistence;

namespace Streetcode.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        try
        {
            var streetcodeContext = app.Services.GetRequiredService<StreetcodeDbContext>();
            await streetcodeContext.Database.MigrateAsync();

            var jwtSettings = app.Services.GetRequiredService<IConfiguration>().GetSection("JWT");
            var secretKey = jwtSettings.GetValue<string>("Secret");

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during startup migration");
        }
    }
}
