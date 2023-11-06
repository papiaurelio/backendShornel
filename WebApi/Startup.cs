using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        services.AddDbContext<StoreDbContext>(opt =>
        {
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddTransient<IProductoRepository, ProductoRepository>();
        services.AddControllers();
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
        App.UseAuthentication();
        App.UseAuthorization();
        App.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}

