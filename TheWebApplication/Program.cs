using System.IO;
using System.Threading.RateLimiting;
using AutoMapper;
using Cache;
using DataAccessEF;
using DataAccessEF.UnitOfWork;
using Domain.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Middleware;
using MinimalApi.Endpoint.Configurations.Extensions;
using Services;
using Services.Interfaces;

namespace TheWebApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EntitiesToDtoMappingProfile());
            mc.AddProfile(new DtoToViewModelMappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();

        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Configuration.AddConfigurationFile("appsettings.json");
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddScoped<RateLimitingAttribute>();
        builder.Services.AddTransient<ICharacteristicService, CharacteristicService>();
        builder.Services.AddSingleton<IRateLimitingCache, RateLimitingCache>();
        builder.Services.AddDbContext<TVShowContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
        );

        var app = builder.Build();

        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Characteristic}/{action=Index}/{id?}");
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot/js")),
            RequestPath = "/js"
        });

        app.Run();
    }
}

