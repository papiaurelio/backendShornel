namespace WebApi;

using BusinessLogic.Data;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<StoreDbContext>();
                await context.Database.MigrateAsync();
                await StoreDbContextData.CargarDataAsync(context, loggerFactory);

                //seguridad

                var userManager = services.GetRequiredService<UserManager<Usuario>>();
                //role
                var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var identityContext = services.GetRequiredService<SeguridadDbContext>();
                await identityContext.Database.MigrateAsync();
                await SeguridadDbContextData.SeedUsersAsync(userManager, rolManager);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "Error en el proceso de migrations");
            }
        }

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
            webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
