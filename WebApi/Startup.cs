using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Dtos.AutoMapper;
using WebApi.Middleware;

namespace WebApi;

public class Startup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //creacion entidades Seguridad
        var builder = services.AddIdentityCore<Usuario>();
        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddEntityFrameworkStores<SeguridadDbContext>();
        builder.AddSignInManager<SignInManager<Usuario>>();

        services.AddAuthentication();

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        services.AddDbContext<StoreDbContext>(opt =>
        {
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        //stringConnection seguridad
        services.AddDbContext<SeguridadDbContext>(x =>
        {
            x.UseSqlServer(Configuration.GetConnectionString("IdentitySeguridad"));
        });
        services.AddTransient<IProductoRepository, ProductoRepository>();
        services.AddControllers();

        //Que los endpoints sean publicos
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsRule", rule =>
            {
                rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
            });
        });
    }

    public void Configure(IApplicationBuilder App, IWebHostEnvironment env)
    {
        //if (env.IsDevelopment())
        //{
        //    App.UseDeveloperExceptionPage();
        //}

        //El codigo de arriba ahora lo hace el Middleware que se esta implementando
        App.UseMiddleware<ExceptionMiddleware>();

        App.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

        App.UseRouting();
        App.UseCors("CorsRule");
        App.UseAuthentication();
        App.UseAuthorization();
        App.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}

