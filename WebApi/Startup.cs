using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenServices>();
        services.AddScoped<IOrdenCompraServices, OrdenComprasServices>();
        //creacion entidades Seguridad
        var builder = services.AddIdentityCore<Usuario>();
        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddRoles<IdentityRole>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Configuración de opciones de usuario predeterminadas.

            // Se establece el conjunto de caracteres permitidos para los nombres de usuario.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // Se establece la opción para requerir que los correos electrónicos de usuario sean únicos.
            options.User.RequireUniqueEmail = true;
        });


        builder.AddEntityFrameworkStores<SeguridadDbContext>();
        builder.AddSignInManager<SignInManager<Usuario>>();


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
            (options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        services.AddScoped(typeof(IGenericSeguridadRepository<>), (typeof(GenericSeguridadRepository<>)));
        services.AddDbContext<StoreDbContext>(opt =>
        {
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        //stringConnection seguridad
        services.AddDbContext<SeguridadDbContext>(x =>
        {
            x.UseSqlServer(Configuration.GetConnectionString("IdentitySeguridad"));
        });

        services.AddSingleton<IConnectionMultiplexer>(c => {
            var configuracion = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configuracion);
        });

        services.TryAddSingleton<ISystemClock, SystemClock>();
        services.AddTransient<IProductoRepository, ProductoRepository>();
        services.AddScoped<ICarritoCompraRepository, CarritoCompraRepository>();
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
        //Authentication...
        App.UseAuthentication();
        App.UseAuthorization();
        App.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}

