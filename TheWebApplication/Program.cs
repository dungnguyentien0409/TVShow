using System.Threading.RateLimiting;
using Cache;
using DataAccessEF;
using DataAccessEF.UnitOfWork;
using Domain.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using MinimalApi.Endpoint.Configurations.Extensions;
using Services.Implementation;
using Services.Interfaces;

namespace TheWebApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Configuration.AddConfigurationFile("appsettings.test.json");

        builder.Services.AddDbContext<TVShowContext>(options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(Services.MappingProfile).Assembly);

        builder.Services.AddTransient<ICharacteristicService, CharacteristicService>();
        builder.Services.AddSingleton<IRateLimitingCache, RateLimitingCache>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Characteristic/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Characteristic}/{action=Index}/{id?}");

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Styles")),
            RequestPath = "/Styles"
        });
        app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
                RequestPath = "/Scripts"
            }
        );

        app.Run();
    }
}

